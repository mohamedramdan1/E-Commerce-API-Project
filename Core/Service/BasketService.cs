using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.BsketModuleDTos;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDTo> CreateOrUpdateBasketAsync(BasketDTo basket)
        {
            var CustomerBasket = _mapper.Map<BasketDTo , CustomerBasket>(basket);
            var CreateOrUpdateBasket = await _basketRepository.CreateOrUpdateAsync(CustomerBasket);
            if (CreateOrUpdateBasket is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can Not Update Or Create Baket Now , Try Again Later");
        }
        public async Task<BasketDTo> GetBasketAsync(string key)
        {
            var Basket = await _basketRepository.GetBasketAsync(key);
            if (Basket is not null)
                return _mapper.Map<CustomerBasket, BasketDTo>(Basket);
            else
                throw new BasketNotFoundException(key);
        }
        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _basketRepository.DeleteAsync(key);
        }
    }
}
