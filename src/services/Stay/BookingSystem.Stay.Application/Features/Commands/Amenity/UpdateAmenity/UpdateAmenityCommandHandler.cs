using AutoMapper;
using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Domain.Entities;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Amenity.UpdateAmenity;

public class UpdateAmenityCommandHandler(IAmenityRepository amenityRepository, IMapper mapper) 
    : IRequestHandler<UpdateAmenityCommand, bool>
{
    private readonly IAmenityRepository _amenityRepository = amenityRepository ?? throw new ArgumentNullException(nameof(amenityRepository));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<bool> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
    {
        AmenityEntity amenity = _mapper.Map<AmenityEntity>(request);

        return await _amenityRepository.UpdateAmenity(amenity, cancellationToken)
            .ConfigureAwait(false);
    }
}