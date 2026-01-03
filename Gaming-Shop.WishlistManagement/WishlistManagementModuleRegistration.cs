
using Gaming_Shop.WishlistManagement.Interfaces;
using Microsoft.Extensions.Configuration;

using Gaming_Shop.WishlistManagement.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Gaming_Shop.WishlistManagement
{
    public static class WishManagementModuleRegistration
    {
        public static void AddWishlistManagementModule(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped<IWishlistService, WishlistService>();
            
        }
    }
}
