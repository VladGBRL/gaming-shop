using Microsoft.AspNetCore.Mvc;
using PaymentManagement.Entities.PaymentManagement.Data;
using PaymentManagement.Services;

namespace Gaming_Shop.Server.Controllers.PaymentManagement
{
    [ApiController]
    [Route("api/stripe")]
    public class PaymentController : ControllerBase
    {
        private readonly StripePaymentService _stripeService;

        public PaymentController(StripePaymentService stripeService)
        {
            _stripeService = stripeService;
        }
        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession(int userId, int cartId)
        {
            var url = await _stripeService.CreateCheckoutSessionAsync(userId,cartId);
            return Ok(new { url });
        }
        [HttpGet("all")]
        public async Task<ActionResult<List<Payment>>> GetAllPayments()
        {
            var payments = await _stripeService.GetAllPaymentsAsync();
            return Ok(payments);
        }
    }
}
