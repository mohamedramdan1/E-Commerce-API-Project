using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects.OrderDTos;

namespace Service.MappingProfiles
{
    internal class OrderItemPictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDTo, string>
    {
        public string Resolve(OrderItem source, OrderItemDTo destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;  // " "
            else
            {
                //var Url = $"https://localhost:7088/{source.PictureUrl}";
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";
                return Url;
            }
        }
    }

}
