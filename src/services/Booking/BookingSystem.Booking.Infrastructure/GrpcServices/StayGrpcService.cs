using BookingSystem.Booking.Infrastructure.Abstractions;
using BookingSystem.Booking.Infrastructure.ViewModel;
using BookingSystem.Stay.gRPC;

namespace BookingSystem.Booking.Infrastructure.GrpcServices;

public class StayGrpcService(StayService.StayServiceClient stayService) : IStayGrpcService
{
    private readonly StayService.StayServiceClient _stayService = stayService;

    public async Task<StayViewModel> GetStayById(int stayId)
    {
        GetStayResponse response = await _stayService.GetStayByIdAsync(new GetStayQuery()
        {
            StayId = stayId
        }).ConfigureAwait(false);

        if(response != null)
        {
            return new StayViewModel()
            {
                Id = response.StayId,
                Address = response.Address,
                Name = response.Name,
                NumberOfBathrooms = response.NumberOfBathrooms,
                NumberOfBedrooms = response.NumberOfBedrooms,
                NumberOfBeds = response.NumberOfBeds,
                NumberOfGuests = response.NumberOfGuests,
                PricePerNight = response.PricePerNight,
                Rating = response.Rating
            };
        }

        return new StayViewModel();
    }
}
