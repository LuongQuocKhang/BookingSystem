using AutoMapper;
using BookingSystem.Promotion.Application.Dtos;
using BookingSystem.Promotion.Application.Features.Promotion.Commands.CreatePromotion;
using BookingSystem.Promotion.Application.Features.Promotion.Commands.UpdatePromotion;
using BookingSystem.Promotion.Application.ViewModel;
using BookingSystem.Promotion.Domain.Entities;

namespace BookingSystem.Promotion.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Get Promotions
        CreateMap<PromotionEntity, PromotionViewModel>().ReverseMap();
        CreateMap<StayPromotionViewModel, StayPromotionEntity>().ReverseMap();
        #endregion

        #region Create Promotion
        CreateMap<CreatePromotionCommand, PromotionEntity>().ReverseMap();

        CreateMap<StayPromotionDto, StayPromotionEntity>()
           .ForMember(dest => dest.PromotionId, opt => opt.MapFrom(src => src.PromotionId))
           .ReverseMap();
        #endregion

        #region Update Promotion
        CreateMap<PromotionEntity, PromotionEntity>().ReverseMap();
        CreateMap<UpdatePromotionCommand, PromotionEntity>().ReverseMap();
        #endregion
    }
}
