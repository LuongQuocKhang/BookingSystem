using AutoMapper;
using BookingSystem.Stay.Domain.Entities;

namespace BookingSystem.Stay.gRPC.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GetStayResponse, StayEntity>().ReverseMap();
    }
}
