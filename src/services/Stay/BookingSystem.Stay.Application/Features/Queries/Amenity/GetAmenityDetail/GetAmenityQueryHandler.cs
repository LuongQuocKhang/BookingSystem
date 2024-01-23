using AutoMapper;
using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.ViewModel.Amenity;
using BookingSystem.Stay.Domain.Entities;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Queries.Amenity.GetAmenities;

public class GetAmenityQueryHandler(IAmenityRepository amenityRepository, IMapper mapper)
    : IRequestHandler<GetAmenityQuery, AmenityViewModel?>
{
    private readonly IAmenityRepository _amenityRepository = amenityRepository ?? throw new ArgumentNullException(nameof(amenityRepository));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<AmenityViewModel?> Handle(GetAmenityQuery request, CancellationToken cancellationToken)
    {
        AmenityEntity? amenity = await _amenityRepository.GetAmenityDetail(request.Id)
            .ConfigureAwait(false);

        AmenityViewModel amenityView = _mapper.Map<AmenityViewModel>(amenity);

        return amenityView;
    }
}
