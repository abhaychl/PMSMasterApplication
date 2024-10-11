using PMSMaster.Data.DataContext;
using PMSMaster.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Validation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var openIdServer = builder.Configuration.GetValue("IdUrl", "");

builder.Services.AddDbContext<ApplicationDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<IUserRepository,UserRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .WithExposedHeaders("Access-Control-Allow-Origin");
        });
});
//string configVal = "https://PMSMasteropenid.chlserv.com";
//string configVal = "https://openid.chlserv.com";
builder.Services.AddOpenIddict()
    .AddValidation(options =>
    {
        options.SetIssuer(openIdServer);
        options.AddAudiences("rs_dataEventRecordsApi");

        options.UseIntrospection()
        .SetClientId("rs_dataEventRecordsApi")
        .SetClientSecret("5CC942BE-DBA8-4D9A-9B09-C226D714B190");

        options.UseSystemNetHttp();
        options.UseAspNetCore();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}
app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
