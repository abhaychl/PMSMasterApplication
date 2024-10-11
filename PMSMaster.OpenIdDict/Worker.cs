using PMSMaster.Data.DataContext;
using OpenIddict.Abstractions;
using System.ComponentModel;
using System.Resources;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace PMSMaster.OpenIdDict
{
    public class Worker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
            await context.Database.EnsureCreatedAsync();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("rs_dataEventRecordsApi") is null)
            {
                var description = new OpenIddictApplicationDescriptor
                {
                    ClientId = "rs_dataEventRecordsApi",
                    ClientSecret = "5CC942BE-DBA8-4D9A-9B09-C226D714B190",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Introspection
                    }
                };

                await manager.CreateAsync(description);
            }

            await RegisterScopesAsync(scope.ServiceProvider);

            static async Task RegisterScopesAsync(IServiceProvider provider)
            {
                var manager = provider.GetRequiredService<IOpenIddictScopeManager>();

                if (await manager.FindByNameAsync("mobile") is null)
                {
                    await manager.CreateAsync(new OpenIddictScopeDescriptor
                    {
                        DisplayName = "dataEventRecords API access",
                        Name = "mobile",
                        Resources = {
                        "rs_dataEventRecordsApi"
                    }
                    });
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
