using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Domain.Entities;
using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Commands.SaveStayToWishlist;

public class SaveStayToWishlistCommandHandler : IRequestHandler<SaveStayToWishlistCommand, bool>
{
    private readonly IStayRepository _stayRepository;
    public SaveStayToWishlistCommandHandler(IStayRepository stayRepository)
    {
        _stayRepository = stayRepository;
    }
    public async Task<bool> Handle(SaveStayToWishlistCommand request, CancellationToken cancellationToken)
    {
        StayWishList wishList = new()
        {
            StayId = request.StayId,
            UserId = request.WishlistId
        };

        await _stayRepository.SaveStayToWishList(wishList).ConfigureAwait(false);

        return true;
    }
}
