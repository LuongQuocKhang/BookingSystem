using AutoMapper;
using BookingSystem.Stay.Application.Features.Commands.CreateStay;
using BookingSystem.Stay.Application.Dto;
using BookingSystem.Stay.Domain.Entities;
using BookingSystem.Stay.Application.ViewModel;
using BookingSystem.Stay.Application.Handlers.Commands.UpdateStay;

namespace BookingSystem.Stay.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Create Stay
        CreateMap<CreateStayCommand, StayEntity>().ReverseMap();

        CreateMap<StayAmenityDto, StayAmenityEntity>()
            .ForPath(dest => dest.Amenity!.Name, opt => opt.MapFrom(src => src.Name))
            .ForPath(dest => dest.AmenityId, opt => opt.MapFrom(src => src.AmenityId))
            .ReverseMap();
        CreateMap<RoomRateDto, RoomRateEntity>().ReverseMap();
        CreateMap<StayUnAvailabilityDto, StayUnAvailabilityEntity>().ReverseMap();
        CreateMap<StayImageDto, StayImageEntity>().ReverseMap();
        CreateMap<StayTagDto, StayTagEntity>().ReverseMap();
        #endregion

        #region Get Stays
        CreateMap<StayViewModel, StayEntity>().ReverseMap();

        CreateMap<UpdateStayCommand, StayEntity>().ReverseMap();

        CreateMap<StayAmenityViewModel, StayAmenityEntity>()
            .ForPath(dest => dest.Amenity!.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
        CreateMap<RoomRateViewModel, RoomRateEntity>().ReverseMap();
        CreateMap<StayUnAvailabilityViewModel, StayUnAvailabilityEntity>().ReverseMap();
        CreateMap<StayImageViewModel, StayImageEntity>().ReverseMap();
        CreateMap<StayTagViewModel, StayTagEntity>().ReverseMap();

        #endregion

        #region Get Stay By id
        CreateMap<StayDetailsViewModel, StayEntity>().ReverseMap();
        CreateMap<AmenityEntity, StayAmenityDto>().ReverseMap();
        #endregion
    }
}
