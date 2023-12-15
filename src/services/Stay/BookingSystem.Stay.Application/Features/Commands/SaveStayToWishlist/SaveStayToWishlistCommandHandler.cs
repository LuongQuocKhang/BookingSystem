using BookingSystem.Stay.Application.Contracts.Persistance;
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
        await _stayRepository.SaveStayToWishList(request.StayId, request.WishlistId).ConfigureAwait(false);

        return true;
    }
}
