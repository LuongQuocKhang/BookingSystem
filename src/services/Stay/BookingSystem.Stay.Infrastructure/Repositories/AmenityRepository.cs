using AutoMapper;
using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Domain.Entities;
using BookingSystem.Stay.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Threading;

namespace BookingSystem.Stay.Infrastructure.Repositories;

public class AmenityRepository(StayContext context, IMapper mapper, ILogger<AmenityRepository> logger) 
    : IAmenityRepository
{
    private readonly StayContext _context = context;

    private readonly IMapper _mapper = mapper;

    private readonly ILogger<AmenityRepository> _logger = logger;

    public async Task<int> CreateAmenity(AmenityEntity entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<AmenityEntity> amenity = await _context.Amenities.AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return amenity.Entity.Id;
    }

    public async Task<bool> DeleteAmenity(int id, CancellationToken cancellationToken = default)
    {
        AmenityEntity? amenity = await _context.Amenities
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken)
            .ConfigureAwait(false);

        if (amenity == null) return false;

        amenity.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return true;
    }

    public async Task<IReadOnlyCollection<AmenityEntity>> GetAmenities()
    {
        List<AmenityEntity> amenities = await _context.Amenities
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ToListAsync()
            .ConfigureAwait(false);

        return new ReadOnlyCollection<AmenityEntity>(amenities);
    }

    public async Task<AmenityEntity?> GetAmenity(int id)
    {
        AmenityEntity? amenity = await _context.Amenities
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id &&!x.IsDeleted)
            .ConfigureAwait(false);

        return amenity;
    }

    public async Task<bool> UpdateAmenity(AmenityEntity entity, CancellationToken cancellationToken = default)
    {
        AmenityEntity? amenity = await _context.Amenities
            .FirstOrDefaultAsync(x => x.Id == entity.Id && !x.IsDeleted, cancellationToken)
            .ConfigureAwait(false);

        if (amenity == null) return false;

        amenity.IsDeleted = entity.IsDeleted;
        amenity.Name = entity.Name;
        amenity.Icon = entity.Icon;

        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return true;
    }
}
