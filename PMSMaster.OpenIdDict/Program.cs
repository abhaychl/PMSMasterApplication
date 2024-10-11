using PMSMaster.Data.DataContext;
using PMSMaster.OpenIdDict;
using PMSMaster.OpenIdDict.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpenIddict.Server;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddHttpClient();
builder.Services.AddCors();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient(); // Configure HttpClient as a service

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
    options.UseOpenIddict();
});

builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
        .UseDbContext<ApplicationDBContext>();
    })

    .AddServer(options =>
    {
        options
        .SetTokenEndpointUris("/connect/token")
        .SetIntrospectionEndpointUris("connect/introspect");

        options.AddEventHandler<OpenIddictServerEvents.ExtractTokenRequestContext>(builder =>
        {
            builder.UseInlineHandler(async context =>
            {
                await CustomTokenManager.FindClientByIdAsync(context);
            });
        });

        options.AddEventHandler<OpenIddictServerEvents.ValidateIntrospectionRequestContext>(builder =>
        {
            builder.UseInlineHandler(async context =>
            {
                await CustomTokenManager.Validatesync(context);
            });
        });

        options.AddEventHandler<OpenIddictServerEvents.ApplyIntrospectionResponseContext>(builder =>
        {
            builder.UseInlineHandler(async context =>
            {
                await CustomTokenManager.ApplyValidateAsync(context);
            });
        });

        //options.AllowPasswordFlow();
        options.UseReferenceAccessTokens().UseReferenceRefreshTokens();
        options.AllowClientCredentialsFlow().AllowRefreshTokenFlow();
        options.RegisterScopes("mobile");
        options.SetAccessTokenLifetime(TimeSpan.FromSeconds(60000));//Settheaccesstokenlifetinehere
        options.SetRefreshTokenLifetime(TimeSpan.FromSeconds(60000));//Settherefreshtokenlifeherp
                                                                  // Register the signing and encryption credentials
        //options.AddDevelopmentEncryptionCertificate().AddDevelopmentSigningCertificate();
        options.AddEphemeralEncryptionKey().AddEphemeralSigningKey();
        //options.AddEncryptionKey(new SymmetricSecurityKey(
        //    Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));

        options.UseAspNetCore()
                .EnableAuthorizationEndpointPassthrough()
                .EnableTokenEndpointPassthrough()
                .DisableTransportSecurityRequirement();
    })
.AddValidation(options =>
{
    options.EnableTokenEntryValidation();
});

builder.Services.AddHostedService<Worker>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    //endpoints.MapDefaultControllerRoute();
    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
