using AutoMapper;
using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Domain.Entities;
using Grpc.Core;

namespace BookingSystem.Stay.gRPC.Services
{
    public class StayProtoService(ILogger<StayProtoService> logger, IStayRepository stayRepository, IMapper mapper) : StayService.StayServiceBase
    {
        private readonly ILogger<StayProtoService> _logger = logger;

        private readonly IStayRepository _stayRepository = stayRepository;

        private readonly IMapper _mapper = mapper;

        public override async Task<GetStayResponse> GetStayById(GetStayQuery request, ServerCallContext context)
        {
            StayEntity? stay = await _stayRepository.GetStayById(request.StayId).ConfigureAwait(false);

            GetStayResponse getStayResponse = new();

            if (stay != null)
            {
                getStayResponse.StayId = stay.Id;
                getStayResponse.Address = stay.Address;
                getStayResponse.NumberOfBathrooms = stay.NumberOfBathrooms;
                getStayResponse.NumberOfBeds = stay.NumberOfBeds;
                getStayResponse.NumberOfBedrooms = stay.NumberOfBedrooms;
                getStayResponse.NumberOfGuests = stay.NumberOfGuests;
                getStayResponse.Name = stay.Name;
                getStayResponse.PricePerNight = stay.PricePerNight;
                getStayResponse.Rating = stay.Rating;
            }

            return getStayResponse;
        }
    }
}
