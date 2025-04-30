﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.BasketModule;

namespace DomainLayer.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string key);
        Task<CustomerBasket?> CreateOrUpdateAsync(CustomerBasket basket , TimeSpan? TimeToLive = null);
        Task<bool> DeleteAsync(string id);
    }
}
