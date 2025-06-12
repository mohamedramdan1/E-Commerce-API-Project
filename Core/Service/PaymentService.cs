using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Shared.DataTransferObjects.BsketModuleDTos;
using Stripe;
using Product = DomainLayer.Models.ProducModule.Product;

namespace Service
{
    public class PaymentService(IConfiguration _configuration,
    IBasketRepository _basketRepository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDTo> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            //Configure Stripe : Install Package Stripe.net
            StripeConfiguration.ApiKey = _configuration["StripeSetting:SecretKey"];

            //Get Basket By basket Id
            var Basket = await _basketRepository.GetBasketAsync(BasketId) ?? throw new BasketNotFoundException(BasketId);

            // Get Amount - Get Product for Product Price + Delivery Method Cost
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                item.Price = Product.Price;
            }

            ArgumentNullException.ThrowIfNull(Basket.deliveryMethodId);
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.deliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(Basket.deliveryMethodId.Value);
            Basket.shippingPrice = DeliveryMethod.Price;

            var BasketAmount = (long)(Basket.Items.Sum(item => item.Quantity * item.Price) + DeliveryMethod.Price) * 100;

            //Create Payment Intent[Create - Update]

            var PaymentService = new PaymentIntentService();
            if (Basket.paymentIntentId is null) // Create
            {
                var Options = new PaymentIntentCreateOptions()
                {
                    Amount = BasketAmount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var PaymentIntent = await PaymentService.CreateAsync(Options);
                Basket.paymentIntentId = PaymentIntent.Id;
                Basket.clientSecret = PaymentIntent.ClientSecret;
            }
            else// update
            {
                var Option = new PaymentIntentUpdateOptions() { Amount = BasketAmount };
                await PaymentService.UpdateAsync(Basket.paymentIntentId, Option);
            }

            await _basketRepository.CreateOrUpdateAsync(Basket);

            return _mapper.Map<BasketDTo>(Basket);
        }
    }

}
