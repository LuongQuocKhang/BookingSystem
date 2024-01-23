using AutoMapper;
using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.ViewModel.Amenity;
using BookingSystem.Stay.Domain.Entities;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Queries.Amenity.GetAmenities;

public class GetAmenitiesQueryHandler(IAmenityRepository amenityRepository, IMapper mapper)
    : IRequestHandler<GetAmenitiesQuery, IReadOnlyCollection<AmenityViewModel>>
{
    private readonly IAmenityRepository _amenityRepository = amenityRepository ?? throw new ArgumentNullException(nameof(amenityRepository));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<IReadOnlyCollection<AmenityViewModel>> Handle(GetAmenitiesQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<AmenityEntity> amenities = await _amenityRepository.GetAmenities()
            .ConfigureAwait(false);

        IReadOnlyCollection<AmenityViewModel> amenityViews = _mapper.Map<IReadOnlyCollection<AmenityViewModel>>(amenities);

        return amenityViews;
    }
}
