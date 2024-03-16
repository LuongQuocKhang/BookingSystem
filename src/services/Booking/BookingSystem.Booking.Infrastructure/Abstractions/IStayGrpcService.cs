using BookingSystem.Booking.Infrastructure.ViewModel;

namespace BookingSystem.Booking.Infrastructure.Abstractions;

public interface IStayGrpcService
{
    Task<StayViewModel> GetStayById(int stayId);
}
