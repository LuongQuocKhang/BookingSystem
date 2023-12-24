using AutoMapper;
using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Domain.Entities;
using BookingSystem.Stay.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace BookingSystem.Stay.Infrastructure.Repositories;

public class AmenityRepository(StayContext context, IMapper mapper, ILogger<AmenityRepository> logger) : IAmenityRepository
{
    private readonly StayContext _context = context;

    private readonly IMapper _mapper = mapper;

    private readonly ILogger<AmenityRepository> _logger = logger;

    public Task<bool> CreateAmenity(AmenityEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAmenity(int id)
    {
        AmenityEntity? amenity = await _context.Amenities
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            .ConfigureAwait(false);

        if (amenity == null) return false;

        amenity.IsDeleted = true;

        await _context.SaveChangesAsync().ConfigureAwait(false);

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

    public async Task<bool> UpdateAmenity(AmenityEntity entity)
    {
        AmenityEntity? amenity = await _context.Amenities
            .FirstOrDefaultAsync(x => x.Id == entity.Id && !x.IsDeleted)
            .ConfigureAwait(false);

        if (amenity == null) return false;

        amenity.IsDeleted = entity.IsDeleted;
        amenity.Name = entity.Name;
        amenity.Icon = entity.Icon;

        await _context.SaveChangesAsync().ConfigureAwait(false);

        return true;
    }
}
