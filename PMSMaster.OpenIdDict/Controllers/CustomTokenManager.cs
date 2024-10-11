using PMSMaster.Data.DataContext;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server;
using System.ComponentModel.DataAnnotations;

namespace PMSMaster.OpenIdDict.Controllers
{
    public class CustomTokenManager : Controller
    {
        private static readonly IServiceProvider _serviceProvider;
        static CustomTokenManager()
        {
            _serviceProvider = new ServiceCollection()
            .AddDbContext<ApplicationDBContext>()
            // Add any other required services
            .BuildServiceProvider();
        }

        public static async Task<ValidationResult> FindClientByIdAsync(OpenIddictServerEvents.ExtractTokenRequestContext context)
        {
            string clientId = context.Request.ClientId;
            var resultValid = new ValidationResult("Request is invalid");
            var result = ValidationResult.Success;
            var newContext = context.Transaction.GetHttpRequest().HttpContext;

            var applicationManager = newContext.RequestServices.GetRequiredService<IOpenIddictApplicationManager>();
            var application = await applicationManager.FindByClientIdAsync(clientId);

            if (application != null)
            {
                return result;
            }

            var connectionString = "";

            if (string.IsNullOrWhiteSpace(clientId))
                return result;

            //clientId = clientId.Split("#")[0];

            using (var scope = _serviceProvider.CreateScope())
            {
                //var dbConetxt = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                var dbConetxt = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                await applicationManager.CreateAsync(dbConetxt.GetUserDetail(clientId.Split("#")[1]));
            }

            return result;
        }

        public static async Task ApplyValidateAsync(OpenIddictServerEvents.ApplyIntrospectionResponseContext context)
        {
            //var isActive = context.Response.GetParameter("active").Value;
            //var newContext = context.Transaction.GetHttpRequest().HttpContext;
            //var tokenManager = newContext.RequestServices.GetRequiredService<IOpenIddictTokenManager>();
            //var tokenInfo = await tokenManager.FindByReferenceIdAsync(context.Request.Token);

            //if (tokenInfo != null && !Convert.ToBoolean(isActive.Value))
            //{
            //    var tokenModel = tokenInfo as OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreToken;
            //    var autorication = tokenModel.Authorization as OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreAuthorization;

            //    if (autorication != null)
            //    {
            //        /// For Delete Token
            //        return;
            //    }
            //    return;
            //}
            return;
        }

        public static async Task<ValidationResult> Validatesync(OpenIddictServerEvents.ValidateIntrospectionRequestContext context)
        {
            // Retrieve the token identifier from the introspection request
            string tokenIdentifier = context.Request.Token;
            string clientId = context.Principal.GetClaim("client_id");

            var resultvalid = new ValidationResult("Request is invalid");
            var result = ValidationResult.Success;

            if (!IsValidRequest(context))
            {
                var newContext = context.Transaction.GetHttpRequest().HttpContext;
                var applicationManager = newContext.RequestServices.GetRequiredService<IOpenIddictApplicationManager>();
                // Retrieve the token from the token identifier
                var application = await applicationManager.FindByClientIdAsync(clientId);
                var newApp = application as OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreApplication;
                var authorizationList = newApp.Authorizations as
                System.Collections.Generic.ICollection<OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreAuthorization>;

                if (authorizationList != null)
                {
                    var tokenid = authorizationList.ToList()[0].Id;
                    // Delete the token

                    return resultvalid;
                }
                return resultvalid;
            }
            return result;
        }

        private static bool IsValidRequest(OpenIddictServerEvents.ValidateIntrospectionRequestContext context)
        {
            if (context.ClientId != "rs_dataEventRecordsApi" || context.ClientSecret !=
            "5CC942BE-DBA8-4D9A-9B09-C226D714B190")
                return false;

            var expirationDate = context.Principal.GetExpirationDate();

            if (expirationDate < DateTimeOffset.UtcNow)
                return false;

            return true;
        }
    }
}


