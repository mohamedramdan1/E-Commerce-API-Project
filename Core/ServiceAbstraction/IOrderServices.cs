using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.OrderDTos;

namespace ServiceAbstraction
{
    public interface IOrderServices
    {
        // Create Order 
        Task<OrderToReturnDTo> CreateOrderAsync(OrderDTo orderDTo , string Email);

        //Get Delivery Methods
        Task<IEnumerable<DeliveryMethodDTo>> GetDeliveryMethodsAsync();

        //Get All orders
        Task<IEnumerable<OrderToReturnDTo>> GetAllOrderAsync(string Email);

        //Get Order By Id
        Task<OrderToReturnDTo> GetOrderByIdAsync(Guid Id);
    }
}
