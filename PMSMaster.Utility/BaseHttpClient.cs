using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;
using Newtonsoft.Json;

namespace PMSMaster.Utility
{    
    public class BaseHttpClient
    {
        public HttpClient client;
        public IJsonSerializer serializer;

        public BaseHttpClient(HttpClient httpClient, IJsonSerializer jsonSerializer)
        {
            serializer = jsonSerializer;
            client = httpClient;
        }

        public async Task<Result<T>> SendRequestAsync<T>(string token, string method , string uri, object data = null)
        {
            try
            {
                // Create request
                var request = new HttpRequestMessage(new HttpMethod(method), uri);

                if(string.IsNullOrWhiteSpace(token))
                    return new Result<T> { ErrorMessage = $"Token is empty" };

                // Set the Authorization header with the Bearer token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                if (data != null)
                {
                    var jsonbody = serializer.SerializeObject(data);
                    request.Content = new StringContent(jsonbody, Encoding.UTF8, "application/json");
                }

                // Send request and get response
                var response = await client.SendAsync(request);
                string responseData = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Create result from response
                    return await Result<T>.FromHttpResponseMessage(responseData, serializer);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    // 404 error (Not Found).
                    return new Result<T> { ErrorMessage = $"404 Error: The requested resource was not found." };
                }
                else if (response.StatusCode >= HttpStatusCode.BadRequest && response.StatusCode < HttpStatusCode.InternalServerError)
                {
                    // Client-side error.
                    return new Result<T> { ErrorMessage = $"Client-side error: {responseData} , Error code: {response.StatusCode}, Response : {JsonConvert.SerializeObject(response)}" };
                }
                else if (response.StatusCode >= HttpStatusCode.InternalServerError)
                {
                    // Server-side error.
                    return new Result<T> { ErrorMessage = $"Server-side error: {responseData}, Error code: {response.StatusCode}, Response : {JsonConvert.SerializeObject(response)}" };
                }
                else
                {
                    // Handle error cases
                    // Parse the JSON response into a JObject.
                    string combinedErrorMessage = GetErrorMessage(responseData);

                    return new Result<T> { ErrorMessage = $"Error Message: {combinedErrorMessage}" };
                }


            }
            catch (Exception ex)
            {
                return new Result<T> { ErrorMessage = ex.Message};
            }
        }

        private static string GetErrorMessage(string responseBody)
        {
            if(string.IsNullOrWhiteSpace(responseBody))
                return "";

            // Parse the JSON response into a JObject.
            JObject jsonResponse = JObject.Parse(responseBody);

            // Extract error details.
            int statusCode = (int)jsonResponse["status"];
            string title = (string)jsonResponse["title"];

            Console.WriteLine($"Status Code: {statusCode}");
            Console.WriteLine($"Error Title: {title}");

            // Extract and combine all error messages.
            var errors = (JObject)jsonResponse["errors"];
            string combinedErrorMessage = string.Join(" | ", errors.Properties().Select(property =>
            {
                string fieldName = property.Name;
                string errorMessage = Convert.ToString(property.Value?.First); // Assuming the error message is an array of strings
                return $"{fieldName}: {errorMessage}";
            }));
            return combinedErrorMessage;
        }
    }
}
