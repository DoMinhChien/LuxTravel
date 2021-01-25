using System.Collections.Generic;
using System.Threading.Tasks;
using LuxTravel.Api.Core.Services;
using LuxTravel.Model.Dtos;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace LuxTravel.Api.Core.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        public PaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<PaymentConfirmDto> CreatePaymentIntent(decimal amount)
        {
            StripeConfiguration.ApiKey = _configuration.GetSection("StripesSettings:Secretkey").Value;


            var service = new PaymentIntentService();
            
            var options = new PaymentIntentCreateOptions()
            {
                Amount = (long)amount * 100,
                Currency = "usd",
                PaymentMethodTypes =  new List<string> { "card"}
            };
            var intent = await service.CreateAsync(options);
            var result = new PaymentConfirmDto()
            {
                PaymentId = intent.Id,
                ClientSecret = intent.ClientSecret
            };

            return result;
        }
    }
}
