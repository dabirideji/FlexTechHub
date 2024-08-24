using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Application.Services.PaymentGateway;
using System.Threading.Tasks;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create-flutterwave-payment")]
        public async Task<IActionResult> CreateFlutterwavePayment([FromBody]InventoryManagement.Application.Services.PaymentGateway.PaymentService.PaymentRequest paymentRequest)
        {
            var paymentLink = await _paymentService.CreateFlutterwavePaymentLink(paymentRequest);
            return Ok(paymentLink);
        }

        [HttpPost("create-paystack-payment")]
        public async Task<IActionResult> CreatePaystackPayment([FromBody]InventoryManagement.Application.Services.PaymentGateway.PaymentService.PaymentRequest paymentRequest)
        {
            Console.WriteLine("starting payment");
            var paymentLink = await _paymentService.CreatePaystackPaymentLink(paymentRequest);
            return Ok(paymentLink);
        }
    }
}
