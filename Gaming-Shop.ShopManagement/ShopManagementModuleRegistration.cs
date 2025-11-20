
using Gaming_Shop.ShopManagement.Interfaces;
using Microsoft.Extensions.Configuration;

using Gaming_Shop.ShopManagement.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Gaming_Shop.ShopManagement
{
    public static class ShopManagementModuleRegistration
    {
        public static void AddShopManagementModule(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped<IProductServices, ProductServices>();
            serviceCollection.AddScoped<ISupplierServices, SupplierServices>();
            serviceCollection.AddScoped<ICategoryServices, CategoryServices>();
        }
    }
}
