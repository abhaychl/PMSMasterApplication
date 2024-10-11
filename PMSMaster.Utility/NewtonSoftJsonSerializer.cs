using Newtonsoft.Json;

namespace PMSMaster.Utility
{
    public interface IJsonSerializer
    {
        string SerializeObject(object obj);
        T DeserializeObject<T>(string value);

    }
    public class NewtonSoftJsonSerializer:IJsonSerializer
    {
        public string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        
        public T DeserializeObject<T>(string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch(Exception ex)
            {
                return JsonConvert.DeserializeObject<T>("error");
            }


        }
    }
}
