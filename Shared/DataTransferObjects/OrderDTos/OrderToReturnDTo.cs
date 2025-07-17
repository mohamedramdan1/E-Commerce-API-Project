using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.IdentityDtos;

namespace Shared.DataTransferObjects.OrderDTos
{
    public class OrderToReturnDTo
    {
        public Guid Id { get; set; }
        public string buyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; }
        public AddressDTo shipToAddress { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public decimal deliveryCost { get; set; } = default!;
        public string Status { get; set; } = default!;
        public ICollection<OrderItemDTo> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
