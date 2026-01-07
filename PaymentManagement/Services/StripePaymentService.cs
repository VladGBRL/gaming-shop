using Gaming_Shop.AccountManagement.Data;
using Gaming_Shop.CartManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PaymentManagement.Data;
using PaymentManagement.Entities;
using PaymentManagement.Entities.PaymentManagement.Data;
using PaymentManagement.Interfaces;
using Stripe.Checkout;

namespace PaymentManagement.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly StripeConfig _stripeConfig;
        private readonly PaymentDbContext _context;
        private readonly CartDbContext _cartContext;
        private readonly AccountManagementDbContext _accountDbContext;

        public StripePaymentService(
            IOptions<StripeConfig> stripeConfig,
            PaymentDbContext context,
            CartDbContext cartContext,
            AccountManagementDbContext accountDbContext
            )
        {
            _stripeConfig = stripeConfig.Value;
            _context = context;
            _cartContext = cartContext;
            _accountDbContext = accountDbContext;

        }

        public async Task<string> CreateCheckoutSessionAsync(
            int userId,
            int cartId)
        {
            var user = await _accountDbContext.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("User not found");
            var cartItems = _cartContext.CartItems
                .Where(ci => ci.CartId == cartId)
                .ToList();

            if (!cartItems.Any())
                throw new Exception("Cart is empty");

            var payment = new Payment
            {
                UserId = userId,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                TotalAmount = cartItems.Sum(i => i.Price * i.Quantity),
                Items = cartItems.Select(ci => new PaymentItem
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.ProductName,
                    UnitPrice = ci.Price,
                    Quantity = ci.Quantity
                }).ToList()
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            var lineItems = cartItems.Select(ci => new SessionLineItemOptions
            {
                Quantity = ci.Quantity,
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "ron",
                    UnitAmount = (long)(ci.Price * 100),
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = ci.ProductName
                    }
                }
            }).ToList();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = $"{_stripeConfig.SuccessUrl}?paymentId={payment.Id}",
                CancelUrl = _stripeConfig.CancelUrl,
                Metadata = new Dictionary<string, string>
                {
                    { "paymentId", payment.Id.ToString() }
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            payment.StripeSessionId = session.Id;
            await _context.SaveChangesAsync();

            return session.Url!;
        }
       public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                .Include(p => p.Items)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}
