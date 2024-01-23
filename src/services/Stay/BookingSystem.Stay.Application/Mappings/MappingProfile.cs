using AutoMapper;
using BookingSystem.Stay.Application.Dto;
using BookingSystem.Stay.Domain.Entities;
using BookingSystem.Stay.Application.ViewModel;
using BookingSystem.Stay.Application.Features.Commands.Stay.CreateStay;
using BookingSystem.Stay.Application.Features.Commands.Stay.UpdateStay;
using BookingSystem.Stay.Application.Dtos.Stay;
using BookingSystem.Stay.Application.ViewModel.Amenity;
using BookingSystem.Stay.Application.Features.Commands.Amenity.CreateAmenity;
using BookingSystem.Stay.Application.Features.Commands.Amenity.UpdateAmenity;

namespace BookingSystem.Stay.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Create Stay
        CreateMap<CreateStayCommand, StayEntity>().ReverseMap();

        CreateMap<StayAmenityDto, StayAmenityEntity>()
            .ForMember(dest => dest.AmenityId, opt => opt.MapFrom(src => src.AmenityId))
            .ReverseMap();

        CreateMap<RoomRateDto, RoomRateEntity>().ReverseMap();
        CreateMap<StayUnAvailabilityDto, StayUnAvailabilityEntity>().ReverseMap();
        CreateMap<StayImageDto, StayImageEntity>().ReverseMap();
        CreateMap<StayTagDto, StayTagEntity>().ReverseMap();
        CreateMap<HostDto, HostEntity>().ReverseMap();
        #endregion

        #region Get Stays
        CreateMap<StayViewModel, StayEntity>().ReverseMap();

        CreateMap<StayAmenityViewModel, StayAmenityEntity>()
            .ReverseMap();

        CreateMap<StayAmenityEntity, StayAmenityViewModel>()
            .ForPath(dest => dest.Name, opt => opt.MapFrom(src => src.Amenity!.Name))
            .ForPath(dest => dest.Icon, opt => opt.MapFrom(src => src.Amenity!.Icon))
            .ReverseMap();

        CreateMap<StayRoomRateViewModel, RoomRateEntity>().ReverseMap();
        CreateMap<StayReviewViewModel, StayReviewEntity>().ReverseMap();
        CreateMap<StayUnAvailabilityViewModel, StayUnAvailabilityEntity>().ReverseMap();
        CreateMap<StayImageViewModel, StayImageEntity>().ReverseMap();
        CreateMap<StayTagViewModel, StayTagEntity>().ReverseMap();
        #endregion

        #region Get Stay By id
        CreateMap<StayDetailsViewModel, StayEntity>().ReverseMap();
        CreateMap<HostViewModel, HostEntity>().ReverseMap();
        #endregion

        #region Update Stay
        CreateMap<UpdateStayCommand, StayEntity>().ReverseMap();
        #endregion

        CreateMap<StayEntity, StayEntity>().ReverseMap();

        #region Get Amenities
        CreateMap<AmenityEntity, AmenityViewModel>().ReverseMap();
        #endregion

        #region Create Amenity
        CreateMap<CreateAmenityCommand, AmenityEntity>().ReverseMap();
        #endregion

        #region Update Amenity
        CreateMap<UpdateAmenityCommand, AmenityEntity>().ReverseMap();
        #endregion
    }
}
