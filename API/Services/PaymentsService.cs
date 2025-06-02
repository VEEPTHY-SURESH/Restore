using API.Entities;
using Stripe;

namespace API.Services;

//Iconfiguration is used to access the keys inside configuration
public class PaymentsService(IConfiguration config)
{
    public async Task<PaymentIntent> CreateOrUpdatePaymentIntent(Basket basket)
    {
        StripeConfiguration.ApiKey = config["StripeSettings:SecretKey"];

        var service = new PaymentIntentService();

        var intent = new PaymentIntent();

        var subtotal = basket.Items.Sum(x => x.Quantity * x.Product.Price);
        var delivaryFee = subtotal > 10000 ? 0 : 500;

        if (string.IsNullOrEmpty(basket.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = subtotal + delivaryFee,
                Currency = "usd",
                PaymentMethodTypes = ["card"],
                Description = "Purchase from Veepthy's Store"
            };
            intent = await service.CreateAsync(options);
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = subtotal + delivaryFee
            };

            await service.UpdateAsync(basket.PaymentIntentId, options);
        }

        return intent;
    }
}