using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BsketModuleDTos;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager _serviceManager) : ApiBaseController
    {
        //Create Or Update Payment Intent Id
        [Authorize]
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDTo>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var Basket = await _serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(BasketId);
            return Ok(Basket);
        }

    }
}
