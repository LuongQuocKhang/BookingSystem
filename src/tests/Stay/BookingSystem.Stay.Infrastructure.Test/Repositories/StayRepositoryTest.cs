using AutoMapper;
using BookingSystem.Promotion.Domain.Entities;
using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Domain.Entities;
using BookingSystem.Stay.Infrastructure.Abstractions;
using BookingSystem.Stay.Infrastructure.Persistance;
using BookingSystem.Stay.Infrastructure.Repositories;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace BookingSystem.Stay.Infrastructure.Test.Repositories;

public class StayRepositoryTest
{
    private readonly Mock<IStayDbContext> _mockContext = new Mock<IStayDbContext>();
    private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
    private readonly Mock<ILogger<StayRepository>> _logger = new Mock<ILogger<StayRepository>>();

    private static DbSet<T> MockDbSet<T>(IQueryable<T> data) where T : class
    {
        Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();

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
    public async Task GetStays_SouldReturnListOfStays()
    {
        IQueryable<StayEntity> stays = new List<StayEntity>()
        {
            new StayEntity()
            {
                Id = 1,
                Name = "TEST Stay",
                Address = "10341",
                CancellationPolicy = "none",
                CheckInTime = "14:00 PM",
                CheckOutTime = "12:00 PM"
            }
        }.AsQueryable();

        _mockContext.Setup(c => c.Stays).Returns(MockDbSet(stays));

        StayRepository stayRepository = new StayRepository(_mockContext.Object,
            _mapper.Object,
            _logger.Object);

        IReadOnlyCollection<StayEntity> stayViews = await stayRepository.GetStays().ConfigureAwait(false);

        Assert.NotNull(stayViews);

        Assert.Single(stayViews);
    }

    [Fact]
    public async Task GetStayById_ValidStayId_ShouldReturnStayDetail()
    {
        // Arrange
        int stayId = 1;

        var stayService = new StayRepository(_mockContext.Object, _mapper.Object, _logger.Object);

        IQueryable<StayEntity> stays = new List<StayEntity>
        {
            new() { Id = stayId, IsDeleted = false },
            new() { Id = 2, IsDeleted = false },
            new() { Id = 3, IsDeleted = true }
        }.AsQueryable();

        _mockContext.Setup(x => x.Stays).Returns(MockDbSet(stays));

        // Act
        var result = await stayService.GetStayById(stayId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(stayId, result.Id);
        Assert.False(result.IsDeleted);
    }

    [Fact]
    public async Task DeleteStay_ValidStayId_ShouldDeleteStay()
    {
        int stayId = 2;
        var contextMock = new Mock<IStayDbContext>();
        var stayService = new StayRepository(contextMock.Object, _mapper.Object, _logger.Object);

        IQueryable<StayEntity> stays = new List<StayEntity>
        {
            new() { Id = stayId, IsDeleted = false },
            new() { Id = 2, IsDeleted = false },
            new() { Id = 3, IsDeleted = true }
        }.AsQueryable();

        contextMock.Setup(x => x.Stays).Returns(MockDbSet(stays));

        bool result = await stayService.DeleteStay(stayId).ConfigureAwait(false);

        contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteStay_InValidStayId_ShouldReturnFalse()
    {
        int stayId = 0;
        
        var stayService = new StayRepository(_mockContext.Object, _mapper.Object, _logger.Object);

        IQueryable<StayEntity> stays = new List<StayEntity>
        {
            new() { Id = stayId, IsDeleted = false },
            new() { Id = 2, IsDeleted = false },
            new() { Id = 3, IsDeleted = true }
        }.AsQueryable();

        _mockContext.Setup(x => x.Stays).Returns(MockDbSet(stays));

        bool result = await stayService.DeleteStay(stayId).ConfigureAwait(false);

        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);

        Assert.False(result);
    }

    [Fact]
    public async Task CreateStay_validInput_ShouldReturnNewStayId()
    {
        var stayService = new StayRepository(_mockContext.Object, _mapper.Object, _logger.Object);

        StayEntity stay = new StayEntity()
        {
            Name = "[TEST] Stay 1",
            Address = "10341",
            CancellationPolicy = "none",
            CheckInTime = "14:00 PM",
            CheckOutTime = "12:00 PM",
            RoomRates = new List<RoomRateEntity>()
            {
                new RoomRateEntity()
                {
                    Value = "RoomRateEntity"
                }
            },
            StayAmenities = new List<StayAmenityEntity>()
            {
                new StayAmenityEntity()
                {
                    AmenityId = 1,
                }
            }

        };

        IQueryable<StayEntity> stays = new List<StayEntity>
        {
            new() { Id = 1, IsDeleted = false },
            new() { Id = 2, IsDeleted = false },
            new() { Id = 3, IsDeleted = true }
        }.AsQueryable();

        _mockContext.Setup(x => x.Stays).Returns(MockDbSet(stays));

        int stayId = await stayService.CreateStay(stay).ConfigureAwait(false);

        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateStay_validInput_ShouldReturnTrue()
    {
        var stayService = new StayRepository(_mockContext.Object, _mapper.Object, _logger.Object);

        StayEntity stay = new StayEntity()
        {
            Id = 1,
            Name = "[TEST] Stay 1",
            Address = "10341",
            CancellationPolicy = "none",
            CheckInTime = "14:00 PM",
            CheckOutTime = "12:00 PM",
            RoomRates = new List<RoomRateEntity>()
            {
                new RoomRateEntity()
                {
                    Value = "RoomRateEntity"
                }
            },
            StayAmenities = new List<StayAmenityEntity>()
            {
                new StayAmenityEntity()
                {
                    AmenityId = 1,
                }
            }

        };

        IQueryable<StayEntity> stays = new List<StayEntity>
        {
            new() { Id = 1, IsDeleted = false },
            new() { Id = 2, IsDeleted = false },
            new() { Id = 3, IsDeleted = true }
        }.AsQueryable();

        _mockContext.Setup(x => x.Stays).Returns(MockDbSet(stays));
        _mockContext.Setup(x => x.StayImages).Returns(MockDbSet(new List<StayImageEntity>().AsQueryable()));
        _mockContext.Setup(x => x.RoomRates).Returns(MockDbSet(new List<RoomRateEntity>().AsQueryable()));
        _mockContext.Setup(x => x.StayAmenities).Returns(MockDbSet(new List<StayAmenityEntity>().AsQueryable()));
        _mockContext.Setup(x => x.StayUnAvailability).Returns(MockDbSet(new List<StayUnAvailabilityEntity>().AsQueryable()));
        _mockContext.Setup(x => x.StayTags).Returns(MockDbSet(new List<StayTagEntity>().AsQueryable()));

        bool result = await stayService.UpdateStay(stay).ConfigureAwait(false);

        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        Assert.True(result);
    }

    [Fact]
    public async Task UpdateStay_InValidInput_ShouldReturnFalse()
    {
        var stayService = new StayRepository(_mockContext.Object, _mapper.Object, _logger.Object);

        StayEntity stay = new StayEntity()
        {
            Id = 9,
            Name = "[TEST] Stay 1",
            Address = "10341",
            CancellationPolicy = "none",
            CheckInTime = "14:00 PM",
            CheckOutTime = "12:00 PM",
            RoomRates = new List<RoomRateEntity>()
            {
                new RoomRateEntity()
                {
                    Value = "RoomRateEntity"
                }
            },
            StayAmenities = new List<StayAmenityEntity>()
            {
                new StayAmenityEntity()
                {
                    AmenityId = 1,
                }
            }

        };

        IQueryable<StayEntity> stays = new List<StayEntity>
        {
            new() { Id = 1, IsDeleted = false },
            new() { Id = 2, IsDeleted = false },
            new() { Id = 3, IsDeleted = true }
        }.AsQueryable();

        _mockContext.Setup(x => x.Stays).Returns(MockDbSet(stays));
        _mockContext.Setup(x => x.StayImages).Returns(MockDbSet(new List<StayImageEntity>().AsQueryable()));
        _mockContext.Setup(x => x.RoomRates).Returns(MockDbSet(new List<RoomRateEntity>().AsQueryable()));
        _mockContext.Setup(x => x.StayAmenities).Returns(MockDbSet(new List<StayAmenityEntity>().AsQueryable()));
        _mockContext.Setup(x => x.StayUnAvailability).Returns(MockDbSet(new List<StayUnAvailabilityEntity>().AsQueryable()));
        _mockContext.Setup(x => x.StayTags).Returns(MockDbSet(new List<StayTagEntity>().AsQueryable()));

        bool result = await stayService.UpdateStay(stay).ConfigureAwait(false);

        Assert.False(result);
    }

    [Fact]
    public async Task ReviewStay_validInput_ShouldReturnTrue()
    {
        var stayService = new StayRepository(_mockContext.Object, _mapper.Object, _logger.Object);

        var review = new StayReviewEntity()
        {
            StayId = 1,
            Comment = "Test comment"
        };

        _mockContext.Setup(x => x.StayReviews).Returns(MockDbSet(new List<StayReviewEntity>()
        {
            review
        }.AsQueryable()));

        bool result = await stayService.ReviewStay(review, It.IsAny<CancellationToken>()).ConfigureAwait(false);

        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task SaveStayToWishList_validInput_ShouldReturnTrue()
    {
        var stayService = new StayRepository(_mockContext.Object, _mapper.Object, _logger.Object);

        var wish = new StayWishListEntity()
        {
            StayId = 1,
            UserId = 1
        };

        _mockContext.Setup(x => x.StayWishLists).Returns(MockDbSet(new List<StayWishListEntity>()
        {
            wish
        }.AsQueryable()));

        bool result = await stayService.SaveStayToWishList(wish, It.IsAny<CancellationToken>()).ConfigureAwait(false);

        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task ShareStay_validInput_ShouldReturnTrue()
    {
        int stayId = 1;
        List<int> receivedUser = new List<int>()
        {
            1, 2
        };

        var stayService = new StayRepository(_mockContext.Object, _mapper.Object, _logger.Object);

        var wish = new StayWishListEntity()
        {
            StayId = 1,
            UserId = 1
        };

        _mockContext.Setup(x => x.StayShares).Returns(MockDbSet(new List<StayShareEntity>()
        {}.AsQueryable()));

        bool result = await stayService.ShareStay(stayId, receivedUser, It.IsAny<CancellationToken>()).ConfigureAwait(false);

        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task AddStayToTrip_validInput_ShouldReturnTrue()
    {
        int stayId = 1, tripId = 1;

        var stayService = new StayRepository(_mockContext.Object, _mapper.Object, _logger.Object);

        var wish = new StayWishListEntity()
        {
            StayId = 1,
            UserId = 1
        };

        _mockContext.Setup(x => x.StayShares).Returns(MockDbSet(new List<StayShareEntity>().AsQueryable()));

        await Assert.ThrowsAsync<NotImplementedException>(async () => await stayService.AddStayToTrip(stayId, tripId, It.IsAny<CancellationToken>()).ConfigureAwait(false));
    }
}
