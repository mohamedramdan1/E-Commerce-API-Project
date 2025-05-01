using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BsketModuleDTos;

namespace Presentation.Controllers
{
    public class BasketController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Get Basket
        [HttpGet]
        public async Task<ActionResult<BasketDTo>> GetBasket(string key)
        {
            var Basket = await _serviceManager.BasketService.GetBasketAsync(key);
            return Ok(Basket);
        }

        // Create Or Update Basket
        [HttpPost]
        public async Task<ActionResult<BasketDTo>> CreateOrUpdateBaket(BasketDTo basket)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }

        //Delete Basket
        [HttpDelete("{key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var Basket = await _serviceManager.BasketService.DeleteBasketAsync(key);
            return Ok(Basket);
        }
    }
}
