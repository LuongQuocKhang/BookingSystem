using AutoMapper;
using BookingSystem.Stay.Application.Contracts.Persistance;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Amenity.DeleteAmenity;

public class DeleteAmenityCommandHandler(IAmenityRepository amenityRepository, IMapper mapper) 
    : IRequestHandler<DeleteAmenityCommand, bool>
{
    private readonly IAmenityRepository _amenityRepository = amenityRepository ?? throw new ArgumentNullException(nameof(amenityRepository));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<bool> Handle(DeleteAmenityCommand request, CancellationToken cancellationToken)
    {
        return await _amenityRepository.DeleteAmenity(request.Id, cancellationToken)
            .ConfigureAwait(false);
    }
}
