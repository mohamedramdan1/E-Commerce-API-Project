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

        Task<OrderToReturnDTo> CreateOrder(OrderDTo orderDTo , string Email);
    }
}
