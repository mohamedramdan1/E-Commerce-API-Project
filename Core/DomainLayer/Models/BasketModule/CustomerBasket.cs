using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.BasketModule
{
    public class CustomerBasket // this class will not be in DataBase 
    {
        public string Id { get; set; } // Guid : Created From Client [FrontEnd]
        public ICollection<BasketItem> Items { get; set; } = []; // it is not navigation property becouse  // this class will not be in DataBase 
        public string? clientSecret { get; set; }
        public string? paymentIntentId { get; set; }
        public int? deliveryMethodId { get; set; }
        public decimal? shippingPrice { get; set; }
    }
}
