using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using System.Data.Common;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using PMSMaster.OpenIdDict;
using System.Text.Json;
using System.Xml;
using PMSMaster.OpenIdDict.Models;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace PMSMaster.Data.DataContext
{
    public class ApplicationDBContext : DbContext
    {
        //private IConfiguration _configuration;
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            //_configuration = configuration;
        }
    //    public class ApplicationDBContext
    //{
       

        //public ApplicationDBContext(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        public OpenIddictApplicationDescriptor GetUserDetail(string clientID)
        {
            var dashBoardList = $"select UserId,Name,ClientSecretKey,LoginID from Users where ClientSecretKey = '{clientID}'";

            //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            string connectionString = "server=103.205.64.106,2499;database=PMSMaster;uid=sa;pwd=BAY48azosW9DrI;Encrypt=False;persist security info=true;multipleactiveresultsets=true;";

            SqlConnection conn = new SqlConnection(connectionString);
            var cmd = new SqlCommand(dashBoardList, conn);
            conn.Open();
            SqlDataReader dtr = cmd.ExecuteReader();

            while (dtr.Read())
            {
                try
                {
                    clientID = $"{Convert.ToString(dtr["UserId"])}#{Convert.ToString(dtr["ClientSecretKey"])}";

                    var openIddictApplicationProperties = new OpenIddictApplicationProperties();
                    openIddictApplicationProperties.UniqueID = Convert.ToString(dtr["UserId"]); openIddictApplicationProperties.LoginID = Convert.ToString(dtr["LoginID"]); openIddictApplicationProperties.UserID = Convert.ToString(dtr["UserId"]); openIddictApplicationProperties.OwnerId = "1";
                    var propertyJson = JsonConvert.SerializeObject(openIddictApplicationProperties, Formatting.Indented, new JsonSerializerSettings { });

                    JsonDocument jsonDocument = JsonDocument.Parse(propertyJson);

                    //Get the root element of the JSON document
                    JsonElement rootElement = jsonDocument.RootElement;
                    var newApplication = new OpenIddictApplicationDescriptor
                    {
                        ClientId = clientID,
                        ClientSecret = Convert.ToString(dtr["ClientSecretKey"]),
                        DisplayName = dtr.GetString("Name"),
                        Permissions =
                        {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.Prefixes.Scope + "offline_access",
                    OpenIddictConstants.Permissions.Prefixes.Scope + "mobile"
                        }
                    };

                    newApplication.Properties["userInfo"] = rootElement;

                    return newApplication;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }


    }
}

