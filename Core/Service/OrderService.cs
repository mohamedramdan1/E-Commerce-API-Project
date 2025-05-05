using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProducModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDtos;
using Shared.DataTransferObjects.OrderDTos;

namespace Service
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderServices
    {
        public async Task<OrderToReturnDTo> CreateOrder(OrderDTo orderDTo, string Email)
        {
            //Map Address To OrderAddress
            var OrderAddress = _mapper.Map<AddressDTo, OrderAddress>(orderDTo.Address);


            //Get Basket
            var Basket = await _basketRepository.GetBasketAsync(orderDTo.BasketId)
                ?? throw new BasketNotFoundException(orderDTo.BasketId);


            //Create OrderItems
            List<OrderItem> OrderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                var OrederItem = new OrderItem()
                {
                    Product = new ProductItemOrdered() { ProductId = Product.Id, PictureUrl = Product.PictureUrl, ProductName = Product.Name },
                    Price = Product.Price,
                    Quantity = item.Quantity,
                };

                OrderItems.Add(OrederItem);
            }


            //Get DeliveryMethod List
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDTo.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDTo.DeliveryMethodId);


            //Calc SubTotal
            var SubTotal = OrderItems.Sum(I => I.Quantity * I.Price);


            //Make Order
            var Order = new Order(Email, OrderAddress, DeliveryMethod, OrderItems, SubTotal);


            //Add Order 
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);
            await _unitOfWork.SaveChange();


            return _mapper.Map<Order, OrderToReturnDTo>(Order);
        }
    }
}
