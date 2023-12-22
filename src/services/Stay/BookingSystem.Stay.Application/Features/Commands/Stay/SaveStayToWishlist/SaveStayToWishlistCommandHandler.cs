using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Domain.Entities;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Stay.SaveStayToWishlist;

public class SaveStayToWishlistCommandHandler : IRequestHandler<SaveStayToWishlistCommand, bool>
{
    private readonly IStayRepository _stayRepository;
    public SaveStayToWishlistCommandHandler(IStayRepository stayRepository)
    {
        _stayRepository = stayRepository;
    }
    public async Task<bool> Handle(SaveStayToWishlistCommand request, CancellationToken cancellationToken)
    {
        StayWishListEntity wishList = new()
        {
            StayId = request.StayId,
            UserId = request.WishlistId
        };

        await _stayRepository.SaveStayToWishList(wishList).ConfigureAwait(false);

        return true;
    }
}
