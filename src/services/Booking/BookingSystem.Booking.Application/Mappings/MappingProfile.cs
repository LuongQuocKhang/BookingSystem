using AutoMapper;
using BookingSystem.Booking.Application.Features.Booking.Commands;
using BookingSystem.Booking.Domain.Entities;

namespace BookingSystem.Booking.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BookingEntity, BookingStayCommand>().ReverseMap();
    }
}
