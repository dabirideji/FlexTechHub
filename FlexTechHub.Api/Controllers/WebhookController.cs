using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Application.Services.PaymentGateway;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public WebhookController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("flutterwave")]
        public async Task<IActionResult> Flutterwave()
        {
            // Convert IHeaderDictionary to IDictionary<string, IEnumerable<string>>
            var headersDict = Request.Headers.ToDictionary(
                header => header.Key,
                header => (IEnumerable<string>)header.Value
            );

            await _paymentService.ProcessFlutterwaveWebhook(Request.Body, headersDict);
            return Ok();
        }

        [HttpPost("paystack")]
        public async Task<IActionResult> Paystack()
        {
            // Convert IHeaderDictionary to IDictionary<string, IEnumerable<string>>
            var headersDict = Request.Headers.ToDictionary(
                header => header.Key,
                header => (IEnumerable<string>)header.Value
            );

            await _paymentService.ProcessPaystackWebhook(Request.Body, headersDict);
            return Ok();
        }
    }
}
