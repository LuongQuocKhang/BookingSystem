using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Commands.SaveStayToWishlist;

public class SaveStayToWishlistCommand : IRequest<bool>
{
    public int StayId { get; set; }
    public int WishlistId { get; set; }
}
