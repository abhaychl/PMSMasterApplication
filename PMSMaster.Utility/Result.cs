
namespace PMSMaster.Utility
{
    public class Result<T>
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success => string.IsNullOrWhiteSpace(ErrorMessage);
        public static async Task<Result<T>> FromHttpResponseMessage(string data, IJsonSerializer jsonOnSerialized)
        {
            var result = new Result<T>();

            if(!string.IsNullOrWhiteSpace(data))
                result.Data = jsonOnSerialized.DeserializeObject<T>(data);
            else
                result.ErrorMessage = "Empty response data.";
            
            return result;
        }
    }
}
