using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace InventoryManagement.Application.Services.PaymentGateway
{
    public class PaymentService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public PaymentService(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public static string ComputeHmacSignature(string data, string secretKey)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        #region FLUTTERWAVE METHODS
        public async Task<string> CreateFlutterwavePaymentLink(PaymentRequest paymentRequest)
        {
            var url = "https://api.flutterwave.com/v3/payments";

            var body = new
            {
                tx_ref = paymentRequest.ReferenceValue.ToString(),
                amount = paymentRequest.Amount,
                currency = paymentRequest.Currency,
                redirect_url = paymentRequest.RedirectUrl,
                payment_options = paymentRequest.PaymentOption,
                customer = new
                {
                    email = paymentRequest.CustomerEmail,
                    phonenumber = string.IsNullOrEmpty(paymentRequest.CustomerPhoneNumber) ? null : paymentRequest.CustomerPhoneNumber,
                    name = string.IsNullOrEmpty(paymentRequest.CustomerName) ? null : paymentRequest.CustomerName
                }
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["PaymentGateway:Flutterwave:SecretKey"]);

            var response = await _httpClient.PostAsync(url, requestContent);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }

        public async Task ProcessFlutterwaveWebhook(Stream requestBody, IDictionary<string, IEnumerable<string>> headers)
        {
            var secretKey = _config["PaymentGateway:Flutterwave:WebhookSecret"];

            using var reader = new StreamReader(requestBody);
            var body = await reader.ReadToEndAsync();

            if (!headers.TryGetValue("x-flutterwave-signature", out var headerValues))
            {
                throw new UnauthorizedAccessException("Missing signature header.");
            }
            var signature = headerValues.FirstOrDefault();
            var computedSignature = ComputeHmacSignature(body, secretKey);

            if (signature != computedSignature)
            {
                throw new UnauthorizedAccessException("Invalid signature.");
            }

            var payload = JsonSerializer.Deserialize<FlutterwaveWebhookPayload>(body);
            var transactionReference = payload.Data.TxRef;
            var status = payload.Data.Status;

            // Update the transaction status in the database
            Console.WriteLine($"Received webhook from Flutterwave: {body}");
        }
        #endregion

        #region PAYSTACK METHODS
        public async Task<string> CreatePaystackPaymentLink(PaymentRequest paymentRequest)
        {
            var url = "https://api.paystack.co/transaction/initialize";

            var body = new
            {
                email = paymentRequest.CustomerEmail,
                amount = paymentRequest.Amount * 100,
                currency = paymentRequest.Currency,
                callback_url = paymentRequest.RedirectUrl
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["PaymentGateway:Paystack:SecretKey"]);

            var response = await _httpClient.PostAsync(url, requestContent);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<PaystackResponse>(responseContent);
            Console.WriteLine($"Response Content: {responseContent}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error from Paystack: {responseContent}");
            }

            return responseContent;
        }

        public async Task ProcessPaystackWebhook(Stream requestBody, IDictionary<string, IEnumerable<string>> headers)
        {
            var secretKey = _config["PaymentGateway:Paystack:WebhookSecret"];

            using var reader = new StreamReader(requestBody);
            var body = await reader.ReadToEndAsync();

            if (!headers.TryGetValue("x-paystack-signature", out var headerValues))
            {
                throw new UnauthorizedAccessException("Missing signature header.");
            }
            var signature = headerValues.FirstOrDefault();
            var computedSignature = ComputeHmacSignature(body, secretKey);

            if (signature != computedSignature)
            {
                throw new UnauthorizedAccessException("Invalid signature.");
            }

            var payload = JsonSerializer.Deserialize<PaystackWebhookPayload>(body);
            var transactionId = payload.Data.Reference;
            var status = payload.Data.Status;

            // Update the transaction status in the database
            Console.WriteLine($"Received webhook from Paystack: {body}");
        }
        #endregion

        #region DATA TRANSFER MODELS
        public class PaymentRequest
        {
            public Guid ReferenceValue { get; set; } = Guid.NewGuid();
            public decimal Amount { get; set; }
            public string? Currency { get; set; } = "NGN";
            public string RedirectUrl { get; set; }
            public string? PaymentOption { get; set; } = "card";
            public string CustomerEmail { get; set; }
            public string? CustomerPhoneNumber { get; set; }
            public string? CustomerName { get; set; }
        }

        #region FLUTTERWAVE MODEL
        public class FlutterwaveResponse
        {
            public FlutterwaveData Data { get; set; }
        }

        public class FlutterwaveData
        {
            public string TxRef { get; set; }
        }

        public class FlutterwaveWebhookPayload
        {
            public FlutterwaveWebhookData Data { get; set; }
        }

        public class FlutterwaveWebhookData
        {
            public string TxRef { get; set; }
            public string Status { get; set; }
        }
        #endregion

        #region PAYSTACK MODEL
        public class PaystackResponse
        {
            public PaystackData Data { get; set; }
        }

        public class PaystackData
        {
            public string Reference { get; set; }
        }

        public class PaystackWebhookPayload
        {
            public PaystackWebhookData Data { get; set; }
        }

        public class PaystackWebhookData
        {
            public string Reference { get; set; }
            public string Status { get; set; }
        }
        #endregion
        #endregion
    }
}
