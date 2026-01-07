using PaymentManagement.Entities.PaymentManagement.Data;
using System;
using System.Threading.Tasks;

namespace PaymentManagement.Interfaces
{
    public interface IStripePaymentService
    {
        Task<string> CreateCheckoutSessionAsync(int userId, int cartId);
        Task<List<Payment>> GetAllPaymentsAsync();
    }
}
