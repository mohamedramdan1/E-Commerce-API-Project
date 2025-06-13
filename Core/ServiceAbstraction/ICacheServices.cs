﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface ICacheServices
    {
        Task<string?> GetAsync(string cacheKey);

        Task SetAsync(string cacheKey, object cacheValue , TimeSpan timeToLive);
    }
}
