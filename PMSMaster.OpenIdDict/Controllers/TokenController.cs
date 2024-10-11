using PMSMaster.OpenIdDict.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using System.Text.Json;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace PMSMaster.OpenIdDict.Controllers
{
    public class TokenController : Controller
    {
        //private readonly IUserRepository _userRepository;

        public TokenController()
        {
           
        }

        [HttpPost("~/connect/token")]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID connect error");

            ClaimsPrincipal claimPrinciple;

            var appManager = HttpContext.RequestServices.GetRequiredService<IOpenIddictApplicationManager>();
            var app = await appManager.FindByClientIdAsync(request.ClientId);

            var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            var sub = await appManager.GetClientIdAsync(app);
            var name = await appManager.GetDisplayNameAsync(app);

            identity.AddClaim(Claims.Subject, sub);
            identity.AddClaim(Claims.Name, name);
            identity.AddClaim(new Claim("DeviceId", "testData"));

            claimPrinciple = new ClaimsPrincipal(identity);

            claimPrinciple.SetScopes(new[]
            {
                Scopes.OfflineAccess,"mobile"
            }.Intersect(request.GetScopes()));

            claimPrinciple.SetScopes(request.GetScopes());

            var scopeManager = HttpContext.RequestServices.GetRequiredService<IOpenIddictScopeManager>();
            claimPrinciple.SetResources(await scopeManager.ListResourcesAsync(claimPrinciple.GetScopes()).TolIstAsync());

            foreach (var claim in claimPrinciple.Claims)
            {
                claim.SetDestinations(GetDestinations(claim, claimPrinciple));
            }

            return SignIn(claimPrinciple, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        private IEnumerable<string> GetDestinations(Claim claim, ClaimsPrincipal principle)
        {
            switch (claim.Type)
            {
                case Claims.Name:
                    yield return Destinations.AccessToken;

                    if (principle.HasScope(Scopes.Profile))
                        yield return Destinations.IdentityToken;

                    yield break;

                case "AspNet.Identity.SecurityStamp":
                    yield break;

                default:
                    yield return Destinations.AccessToken;
                    yield break;
            }
        }

        //public OpenIddictApplicationDescriptor GetUserDetail(string clientID)
        //{
        //    //var user = _userRepository.GetUserByClientSecret(clientID);
        //    var user = new User();

        //    if (user == null)
        //        return null;

        //    try
        //    {
        //        clientID = $"{Convert.ToString(user.UserId)}#{user.ClientSecretKey}";

        //        var openIddictApplicationProperties = new OpenIddictApplicationProperties();
        //        openIddictApplicationProperties.UniqueID = Convert.ToString(user.UserId); openIddictApplicationProperties.LoginID = user.LoginId; openIddictApplicationProperties.UserID = Convert.ToString(user.UserId); openIddictApplicationProperties.OwnerId = "1";
        //        var propertyJson = JsonConvert.SerializeObject(openIddictApplicationProperties, Formatting.Indented, new JsonSerializerSettings { });

        //        JsonDocument jsonDocument = JsonDocument.Parse(propertyJson);

        //        //Get the root element of the JSON document
        //        JsonElement rootElement = jsonDocument.RootElement;
        //        var newApplication = new OpenIddictApplicationDescriptor
        //        {
        //            ClientId = clientID,
        //            ClientSecret = user.ClientSecretKey,
        //            DisplayName = user.Name,
        //            Permissions =
        //                    {
        //                OpenIddictConstants.Permissions.Endpoints.Authorization,
        //                    OpenIddictConstants.Permissions.Endpoints.Token,
        //                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
        //                    OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
        //                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
        //                    OpenIddictConstants.Permissions.Prefixes.Scope + "offline_access",
        //                OpenIddictConstants.Permissions.Prefixes.Scope + "mobile"
        //                    }
        //        };

        //        newApplication.Properties["userInfo"] = rootElement;

        //        return newApplication;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
