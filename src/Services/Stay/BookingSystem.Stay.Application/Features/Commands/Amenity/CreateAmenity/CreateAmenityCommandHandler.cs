using AutoMapper;
using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Domain.Entities;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Amenity.CreateAmenity;

public class CreateAmenityCommandHandler(IMapper mapper, IAmenityRepository amenityRepository) 
    : IRequestHandler<CreateAmenityCommand, int>
{
    private readonly IAmenityRepository _amenityRepository = amenityRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<int> Handle(CreateAmenityCommand request, CancellationToken cancellationToken)
    {
        AmenityEntity amenity = _mapper.Map<AmenityEntity>(request);

        int amenityId = await _amenityRepository.CreateAmenity(amenity)
            .ConfigureAwait(false);

        return amenityId;
    }
}
