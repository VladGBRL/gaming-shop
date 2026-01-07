using Gaming_Shop.ShopManagement.Interfaces;
using Gaming_Shop.ShopManagement.Services;
using Microsoft.Extensions.DependencyInjection;
using PaymentManagement.Interfaces;
using PaymentManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagement
{
    public static class PaymentManagementModuleRegistration
{
        public static void AddStripePaymentManagementModule(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped<IStripePaymentService, StripePaymentService>();
            
        }
    }
}
