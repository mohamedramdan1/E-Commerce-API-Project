using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DataTransferObjects.IdentityDtos;
using Shared.DataTransferObjects.OrderDTos;

namespace Service.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTo, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDTo>()
                .ForMember(dist => dist.DeliveryMethod, Option => Option.MapFrom(Src => Src.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDTo>()
                .ForMember(dist => dist.ProductName, Option => Option.MapFrom(Src => Src.Product.ProductName))
                .ForMember(dist => dist.PictureUrl, Option => Option.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDTo>();

        }
    }
}
