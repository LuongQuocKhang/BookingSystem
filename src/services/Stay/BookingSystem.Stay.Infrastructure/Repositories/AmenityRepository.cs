using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Domain.Entities;
using BookingSystem.Stay.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace BookingSystem.Stay.Infrastructure.Repositories;

public class AmenityRepository(IStayDbContext context, ILogger<AmenityRepository> logger) 
    : IAmenityRepository
{
    private readonly IStayDbContext _context = context;
    private readonly ILogger<AmenityRepository> _logger = logger;

    public async Task<int> CreateAmenity(AmenityEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            EntityEntry<AmenityEntity> amenity = await _context.Amenities.AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return amenity.Entity.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return int.MinValue;
        }
    }

    public async Task<bool> DeleteAmenity(int id, CancellationToken cancellationToken = default)
    {
        AmenityEntity? amenity = _context.Amenities
            .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

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

    public async Task<AmenityEntity?> GetAmenityDetail(int id)
    {
        AmenityEntity? amenity = _context.Amenities
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

        return await Task.FromResult(amenity).ConfigureAwait(false);
    }

    public async Task<bool> UpdateAmenity(AmenityEntity entity, CancellationToken cancellationToken = default)
    {
        AmenityEntity? amenity = _context.Amenities
            .FirstOrDefault(x => x.Id == entity.Id);

        if (amenity == null) return false;

        amenity.IsDeleted = entity.IsDeleted;
        amenity.Name = entity.Name;
        amenity.Icon = entity.Icon;

        _context.Amenities.Update(amenity);

        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return true;
    }
}
