
using Gaming_Shop.CartManagement.Interfaces;
using Microsoft.Extensions.Configuration;

using Gaming_Shop.CartManagement.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Gaming_Shop.CartManagement
{
    public static class CartManagementModuleRegistration
    {
        public static void AddCartManagementModule(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped<ICartService, CartService>();
           
        }
    }
}
