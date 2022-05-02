using AutoMapper;
using ApiAppShop.Domain.Dtos;
using ApiUser.Application.DataContracts;
using ApiAppShop.Domain.Events;

namespace ApiAppShop.Application.Infrastructure.AutoMapper.Profiles
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile()
        {
            CreateMap<PurchaseRequest, AppPurchaseDto>();

            CreateMap<AppPurchaseDto, AppPurchasedEvent>();            
        }
    }
}
