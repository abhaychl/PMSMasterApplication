using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Net;
using System.Text.Json.Nodes;

namespace PMSMaster.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class TokenController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private IConfiguration _configuration;
        public TokenController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Gettoken(string userName, string password)
        {
            //JToken userName = "";
            //JToken password = "";

            HttpClient httpClient = new HttpClient();

            //jobject.TryGetValue("userName", out userName);
            //jobject.TryGetValue("password", out password);

            var userNameVal = Convert.ToString(userName);
            var passwordVal = Convert.ToString(password);

            var keyPariCollection = new Dictionary<string, string>();

            keyPariCollection.Add("username", userNameVal);
           

            var user = _userRepository.GetUserByuserIdPasswor(userNameVal, passwordVal);

            if (user == null)
            {
                var contentresult = new ContentResult();
                System.Net.HttpStatusCode statusCode = HttpStatusCode.Unauthorized;

                contentresult.StatusCode = (int)statusCode;
                contentresult.Content = "Some Error occured";
                return contentresult;

                //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                //response.ReasonPhrase = "User not found";
                //return new HttpResponseMessageResult(response);
            }

            var clientID = $"{Convert.ToString(user.UserId)}#{Convert.ToString(user.ClientSecretKey)}";

            keyPariCollection.Add("client_id", clientID);
            keyPariCollection.Add("password", passwordVal);

            keyPariCollection.Add("client_secret", user.ClientSecretKey);
            keyPariCollection.Add("scope", "mobile");
            keyPariCollection.Add("grant_type", "client_credentials");
            string url = $"{_configuration["IdUrl"]}/connect/token";
            return GenrateToken(httpClient, keyPariCollection, user.UserId, url, user.Role?.Name, user.RoleId, user.TargetAmount,user.Name).Result;
        }

        private static async Task<IActionResult> GenrateToken(HttpClient httpClient, Dictionary<string, string> keyPariCollection, int userId, string url, string? userRole, int? RoleId, double? TargetAmount,string Name)
        {

            var contentresult = new ContentResult();
            System.Net.HttpStatusCode statusCode;

            using (var httpRequestMessage = new HttpRequestMessage())
            {
                httpRequestMessage.Content = new FormUrlEncodedContent(keyPariCollection);
                httpRequestMessage.Method = new HttpMethod("Post");                
                //httpRequestMessage.RequestUri = new Uri("https://localhost:7218/connect/token");
                httpRequestMessage.RequestUri = new Uri(url);

                using (var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false))
                {
                    statusCode = response.StatusCode;

                    var encryptedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    contentresult.StatusCode = (int)statusCode;
                    
                    JObject json = JObject.Parse(encryptedJson);
                    // Add a new property
                    json["UserId"] = userId;
                    json["Role"] = userRole;
                    json["Role"] = userRole;
                    json["RoleId"] = RoleId;
                    json["Target"] = TargetAmount;
                    json["Name"] = Name;

                    contentresult.Content = json.ToString();
                    return contentresult;
                }
            }

            contentresult.StatusCode = (int)statusCode;
            contentresult.Content = "Some Error occured";
            return contentresult;
        }
    }
}
