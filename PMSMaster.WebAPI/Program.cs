using PMSMaster.Data.DataContext;
using PMSMaster.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OpenIddict.Validation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var openIdServer = builder.Configuration.GetValue("IdUrl","");

builder.Services.AddDbContext<ApplicationDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRateCardRepository, RateCardRepository>();

builder.Services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
builder.Services.AddScoped<IOfficeLocationRepository, OfficeLocationRepository>();
builder.Services.AddScoped<IUserVerticalsRepository, UserVerticalsRepository>();

builder.Services.AddScoped<IServicesRepository, ServicesRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IClientIndustriesRepository, ClientIndustriesRepository>();

builder.Services.AddScoped<IDesignationRepository, DesignationRepository>();
builder.Services.AddScoped<IFinancialYearRepository, FinancialYearRepository>();
builder.Services.AddScoped<IFaqCategoryRepository, FaqCategoryRepository>();
builder.Services.AddScoped<IFaqRepository, FaqRepository>();
builder.Services.AddScoped<IDeskTimeRepository, DeskTimeRepository>();


builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientRemarkRepository, ClientRemarkRepository>();

builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IStatesRepository, StatesRepository>();
builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();

builder.Services.AddScoped<IClientOfficeLoactionRepository, ClientOfficeLoactionRepository>();
builder.Services.AddScoped<IClientContactPersonRepository, ClientContactPersonRepository>();



builder.Services.AddScoped<IKPIRuleRepository, KPIRuleRepository>();
builder.Services.AddScoped<IKPIIndicatorRepository, KPIIndicatorRepository>();
builder.Services.AddScoped<IKPIRuleIndicatorRepository, KPIRuleIndicatorRepository>();

builder.Services.AddScoped<IUserGroupingRepository, UserGroupingRepository>();
builder.Services.AddScoped<IUserGroupingUsersRepository, UserGroupingUsersRepository>();

builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<ITranslationToolsRepository, TranslationToolsRepository>();
builder.Services.AddScoped<IDTPCategoryRepository, DTPCategoryRepository>();
builder.Services.AddScoped<ISoftwareCategoryRepository, SoftwareCategoryRepository>();
builder.Services.AddScoped<ICheckListCategoryRepository, CheckListCategoryRepository>();
builder.Services.AddScoped<ICheckListMasterRepository, CheckListMasterRepository>();
builder.Services.AddScoped<IVendorTypeRepository, VendorTypeRepository>();
builder.Services.AddScoped<IRequisitionRepository, RequisitionRepository>();
builder.Services.AddScoped<IVendorCategoryRepository, VendorCategoryRepository>();
builder.Services.AddScoped<IVendorCategoryDocumentRepository, VendorCategoryDocumentRepository>();
builder.Services.AddScoped<ICommonRepository, CommonRepository>();

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
//string configVal = "https://PMSMasteropenid.chlsoftech.com";
//string configVal = "https://PMSMasteropenid.chlserv.com";
//string configVal = "https://localhost:7218";
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
