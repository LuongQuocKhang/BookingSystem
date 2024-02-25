using AutoMapper;
using BookingSystem.Promotion.Application.Constant;
using BookingSystem.Promotion.Domain.Entities;
using BookingSystem.Promotion.Infrastructure.Abstractions;
using BookingSystem.Promotion.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookingSystem.Promotion.Infrastructure.Test.Repositories;

public class PromotionRepositoryTest
{
    private readonly Mock<IPromotionDbContext> _mockContext = new Mock<IPromotionDbContext>();
    private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
    private readonly Mock<ILogger<PromotionRepository>> _logger = new Mock<ILogger<PromotionRepository>>();

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
    public async Task GetPromotions_SouldReturnListOfPromotionDetail()
    {
        IQueryable<PromotionEntity> stays = new List<PromotionEntity>()
        {
            new()
            {
                Id = 1,
                Name = "TEST Promotion"
            }
        }.AsQueryable();

        _mockContext.Setup(c => c.Promotions).Returns(MockDbSet(stays));

        PromotionRepository stayRepository = new PromotionRepository(_mockContext.Object,
            _mapper.Object,
            _logger.Object);

        IReadOnlyCollection<PromotionEntity>? promotions= await stayRepository.GetPromotions(It.IsAny<int>(), 
            It.IsAny<int>(),
            It.IsAny<OrderBy>(),
            It.IsAny<CancellationToken>()).ConfigureAwait(false);

        Assert.NotNull(promotions);
        Assert.Single(promotions);
    }
    
    [Fact]
    public async Task GetPromotionDetail_SouldReturnListOfPromotionDetail()
    {
        int promotionId = 1;

        IQueryable<PromotionEntity> stays = new List<PromotionEntity>()
        {
            new()
            {
                Id = 1,
                Name = "TEST Promotion"
            }
        }.AsQueryable();

        _mockContext.Setup(c => c.Promotions).Returns(MockDbSet(stays));

        PromotionRepository stayRepository = new PromotionRepository(_mockContext.Object,
            _mapper.Object,
            _logger.Object);

        PromotionEntity? promotionDetail = await stayRepository.GetPromotionDetail(promotionId, It.IsAny<CancellationToken>()).ConfigureAwait(false);

        Assert.NotNull(promotionDetail);
        Assert.Equal(1, promotionDetail.Id);
    }
    
    [Fact]
    public async Task GetPromotionsByStay_SouldReturnListOfPromotionsByStay()
    {
        int stayId = 1;

        IQueryable<PromotionEntity> stays = new List<PromotionEntity>()
        {
            new()
            {
                Id = 1,
                Name = "TEST Promotion"
            }
        }.AsQueryable();

        _mockContext.Setup(c => c.Promotions).Returns(MockDbSet(stays));
        _mockContext.Setup(c => c.StayPromotions).Returns(MockDbSet(new List<StayPromotionEntity>() { 
            new()
            {
                StayId = 1
            }
        }.AsQueryable()));

        PromotionRepository stayRepository = new PromotionRepository(_mockContext.Object,
            _mapper.Object,
            _logger.Object);

        IReadOnlyCollection<PromotionEntity>? stayView = await stayRepository.GetPromotionsByStay(stayId, It.IsAny<CancellationToken>()).ConfigureAwait(false);

        Assert.NotNull(stayView);
        Assert.Single(stayView);
    }

    [Fact]
    public async Task CreatePromotion_SouldReturnListOfPromotionDetail()
    {
        var newPromotion = new PromotionEntity()
        {
            Name = "TEST Promotion 2"
        };

        IQueryable<PromotionEntity> stays = new List<PromotionEntity>()
        {
            new()
            {
                Id = 1,
                Name = "TEST Promotion"
            }
        }.AsQueryable();

        _mockContext.Setup(c => c.Promotions).Returns(MockDbSet(stays));

        PromotionRepository stayRepository = new PromotionRepository(_mockContext.Object,
            _mapper.Object,
            _logger.Object);

        int promotionId = await stayRepository.CreatePromotion(newPromotion, It.IsAny<CancellationToken>()).ConfigureAwait(false);

        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdatePromotion_SouldReturnListOfPromotionDetail()
    {
        var updatePromotion = new PromotionEntity()
        {
            Id = 1,
            Name = "TEST Promotion 2"
        };

        IQueryable<PromotionEntity> stays = new List<PromotionEntity>()
        {
            new()
            {
                Id = 1,
                Name = "TEST Promotion"
            }
        }.AsQueryable();

        _mockContext.Setup(c => c.Promotions).Returns(MockDbSet(stays));
        _mockContext.Setup(c => c.StayPromotions).Returns(MockDbSet(new List<StayPromotionEntity>().AsQueryable()));

        PromotionRepository stayRepository = new PromotionRepository(_mockContext.Object,
            _mapper.Object,
            _logger.Object);

        bool result = await stayRepository.UpdatePromotion(updatePromotion, It.IsAny<CancellationToken>()).ConfigureAwait(false);

        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        Assert.True(result);
    }
    
    [Fact]
    public async Task DeletePromotion_SouldReturnListOfPromotionDetail()
    {
        int deletePromotion = 1;

        IQueryable<PromotionEntity> stays = new List<PromotionEntity>()
        {
            new()
            {
                Id = 1,
                Name = "TEST Promotion"
            }
        }.AsQueryable();

        _mockContext.Setup(c => c.Promotions).Returns(MockDbSet(stays));
        _mockContext.Setup(c => c.StayPromotions).Returns(MockDbSet(new List<StayPromotionEntity>().AsQueryable()));

        PromotionRepository stayRepository = new PromotionRepository(_mockContext.Object,
            _mapper.Object,
            _logger.Object);

        bool result = await stayRepository.DeletePromotion(deletePromotion, It.IsAny<CancellationToken>()).ConfigureAwait(false);

        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        Assert.True(result);
    }
}
