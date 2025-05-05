using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DataTransferObjects.IdentityDtos;

namespace Service.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTo, OrderAddress>();

        }
    }
}
