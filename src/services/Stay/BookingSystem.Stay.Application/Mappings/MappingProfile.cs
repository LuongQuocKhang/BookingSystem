using AutoMapper;
using BookingSystem.Stay.Application.Features.Commands.CreateStay;
using BookingSystem.Stay.Application.Dto;
using BookingSystem.Stay.Domain.Entities;
using BookingSystem.Stay.Application.ViewModel;

namespace BookingSystem.Stay.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Create Stay
        CreateMap<CreateStayCommand, StayEntity>().ReverseMap();

        CreateMap<StayAmenityDto, StayAmenityEntity>().ReverseMap();
        CreateMap<RoomRateDto, RoomRateEntity>().ReverseMap();
        CreateMap<StayUnAvailabilityDto, StayUnAvailabilityEntity>().ReverseMap();
        CreateMap<StayImageDto, StayImageEntity>().ReverseMap();
        CreateMap<StayTagDto, StayTagEntity>().ReverseMap();
        #endregion

        #region Get Stay By id
        CreateMap<StayDetailsViewModel, StayEntity>().ReverseMap();
        
        #endregion
    }
}
