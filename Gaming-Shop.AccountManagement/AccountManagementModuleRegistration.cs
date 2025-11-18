
using Gaming_Shop.AccountManagement.Interfaces;
using Gaming_Shop.AccountManagement.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Gaming_Shop.AccountManagement
{
    public static class AccountManagementModuleRegistration
    {
        public static void AddAccountManagementModule(this IServiceCollection serviceCollection, IConfiguration configuration)
        {

            serviceCollection.AddScoped<ITokenService, TokenService>();



        }
    }
}
