using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.OrderDTos;

namespace Presentation.Controllers
{
    public class OrdersController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Create Order
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTo>> CreateOrder(OrderDTo orderDTo)
        {
            var Order = await _serviceManager.OrderServices.CreateOrderAsync(orderDTo , GetEmailFromToken());
            return Ok(Order);
        }

        // Get Delivery Method
        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTo>>> GetDeliveryMethod()
        {
            var Order = await _serviceManager.OrderServices.GetDeliveryMethodsAsync();
            return Ok(Order);
        }

        // Get All Order By Email
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTo>>> GetAllOrder()
        {
            var Order = await _serviceManager.OrderServices.GetAllOrderAsync(GetEmailFromToken());
            return Ok(Order);
        }

        //Get Order By Id
        [Authorize]
        [HttpGet("{id:guid}")] 
        public async Task<ActionResult<OrderToReturnDTo>> GetOrderById(Guid id)
        {
            var Order = await _serviceManager.OrderServices.GetOrderByIdAsync(id);
            return Ok(Order);
        }
    }
}
