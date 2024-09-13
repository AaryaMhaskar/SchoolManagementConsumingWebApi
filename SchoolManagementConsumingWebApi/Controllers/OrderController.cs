using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using SchoolManagementConsumingWebApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagementConsumingWebApi.Controllers
{
    public class OrderController : Controller
    {
        [BindProperty]
        public OrderEntity Orders { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateOrder()
        {
            string key = "rzp_test_4DrJJevYZkd0No";
            string secret = "3oX1Dvxlpy2wB3BGnDPxGtH8";

            Random random = new Random();
            string transactionId = random.Next(0, 3000).ToString();

            var input = new Dictionary<string, object>
            {
                { "amount", Convert.ToDecimal(Orders.Amount) * 100 }, // amount in paise
                { "currency", "INR" },
                { "receipt", transactionId }
            };

            var client = new RazorpayClient(key, secret);
            var orderResponse = client.Order.Create(input);
            var order = (Order)orderResponse;

            ViewBag.OrderId = order.Attributes["id"];
            return View("Payment", Orders);
        }

        public IActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Payment(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            string keySecret = "3oX1Dvxlpy2wB3BGnDPxGtH8";

            var attributes = new Dictionary<string, string>
            {
                { "razorpay_payment_id", razorpay_payment_id },
                { "razorpay_order_id", razorpay_order_id },
                { "razorpay_signature", razorpay_signature }
            };

            // Make sure to replace `Utils.verifyPaymentSignature` with the correct method if it does not exist
            bool isSignatureValid = Utils.GenerateSignature(razorpay_order_id, razorpay_payment_id, keySecret) == razorpay_signature;

            if (isSignatureValid)
            {
                var orders = new OrderEntity
                {
                    TransactionId = razorpay_payment_id,
                    OrderId = razorpay_order_id
                };

                return View("PaymentSuccess", orders);
            }

            return View("PaymentFailure");
        }

        public static class Utils
        {
            public static string GenerateSignature(string orderId, string paymentId, string keySecret)
            {
                string data = $"{orderId}|{paymentId}";
                using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(keySecret)))
                {
                    byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                    return BitConverter.ToString(hash).Replace("-", "").ToLower();
                }
            }
        }
    }
}
