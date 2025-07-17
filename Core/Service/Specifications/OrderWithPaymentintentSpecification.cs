using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModule;

namespace Service.Specifications
{
    internal class OrderWithPaymentintentSpecification : BaseSpecifications<Order, Guid>
    {
        public OrderWithPaymentintentSpecification(string PaymentIntentId) : base(O => O.PaymentIntentId == PaymentIntentId)
        {

        }
    }
}
