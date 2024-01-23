using AutoMapper;
using BookingSystem.Stay.Domain.Entities;
using BookingSystem.Stay.Infrastructure.Abstractions;
using BookingSystem.Stay.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookingSystem.Stay.Infrastructure.Test.Repositories;

public class AmenityRepositoryTest
{
    private readonly Mock<IStayDbContext> _mockContext = new Mock<IStayDbContext>();

    private readonly Mock<ILogger<AmenityRepository>> _logger = new Mock<ILogger<AmenityRepository>>();

    private static DbSet<T> MockDbSet<T>(IQueryable<T> data) where T : class
    {
        Mock<DbSet<T>> dbSetMock = new();

        dbSetMock.As<IAsyncEnumerable<T>>()
            .Setup(x => x.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));

        dbSetMock.As<IQueryable<T>>()
            .Setup(x => x.Provider)
            .Returns(new TestAsyncQueryProvider<T>(data.Provider));

        dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(data.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(data.ElementType);
        dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

        return dbSetMock.Object;
    }


    [Fact]
    public async Task GetAmenities_ValidInput_ShouldReturnListOfAmenities()
    {
        IQueryable<AmenityEntity> amenities = new List<AmenityEntity>()
        {
            new AmenityEntity()
            {
                Id = 1,
                Name = "TEST Amenity",
            }
        }.AsQueryable();

        _mockContext.Setup(c => c.Amenities).Returns(MockDbSet(amenities));

        AmenityRepository stayRepository = new(_mockContext.Object, _logger.Object);

        IReadOnlyCollection<AmenityEntity> stayViews = await stayRepository.GetAmenities()
            .ConfigureAwait(false);

        Assert.NotNull(stayViews);

        Assert.Single(stayViews);
    }
    
    [Fact]
    public async Task GetAmenity_ValidInput_ShouldReturnAmenityDetail()
    {
        int amenityId = 1;

        IQueryable<AmenityEntity> amenities = new List<AmenityEntity>()
        {
            new()
            {
                Id = 1,
                Name = "TEST Amenity",
            }
        }.AsQueryable();

        _mockContext.Setup(c => c.Amenities).Returns(MockDbSet(amenities));

        AmenityRepository stayRepository = new(_mockContext.Object, _logger.Object);

        AmenityEntity? stayViews = await stayRepository.GetAmenityDetail(amenityId)
            .ConfigureAwait(false);

        Assert.NotNull(stayViews);

        Assert.Equal(amenityId, stayViews.Id);
    }
    
    [Fact]
    public async Task CreateAmenity_ValidInput_ShouldReturnAmenityDetail()
    {
        AmenityEntity amenity = new()
        {
            Name = "TEST Amenity 2",
        };

        IQueryable<AmenityEntity> amenities = new List<AmenityEntity>()
        {
            new()
            {
                Id = 1,
                Name = "TEST Amenity",
            }
        }.AsQueryable();

        _mockContext.Setup(c => c.Amenities).Returns(MockDbSet(amenities));

        AmenityRepository stayRepository = new(_mockContext.Object, _logger.Object);

        int amenityId = await stayRepository.CreateAmenity(amenity, It.IsAny<CancellationToken>())
            .ConfigureAwait(false);

        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAmenity_ValidInput_ShouldReturnAmenityDetail()
    {
        AmenityEntity amenity = new()
        {
            Id = 1,
            Name = "TEST Amenity 2",
        };

        IQueryable<AmenityEntity> amenities = new List<AmenityEntity>()
        {
            new()
            {
                Id = 1,
                Name = "TEST Amenity",
            }
        }.AsQueryable();

        _mockContext.Setup(c => c.Amenities).Returns(MockDbSet(amenities));

        AmenityRepository stayRepository = new(_mockContext.Object, _logger.Object);

        bool result = await stayRepository.UpdateAmenity(amenity, It.IsAny<CancellationToken>())
            .ConfigureAwait(false);

        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task DeleteAmenity_ValidInput_ShouldReturnAmenityDetail()
    {
        int amenityId = 1;

        IQueryable<AmenityEntity> amenities = new List<AmenityEntity>()
        {
            new()
            {
                Id = 1,
                Name = "TEST Amenity",
            }
        }.AsQueryable();

        _mockContext.Setup(c => c.Amenities).Returns(MockDbSet(amenities));

        AmenityRepository stayRepository = new(_mockContext.Object, _logger.Object);

        bool result = await stayRepository.DeleteAmenity(amenityId, It.IsAny<CancellationToken>())
            .ConfigureAwait(false);

        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
