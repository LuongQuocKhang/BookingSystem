using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Stay.SaveStayToWishlist;

public class SaveStayToWishlistCommand : IRequest<bool>
{
    public int StayId { get; set; }
    public int WishlistId { get; set; }
}
