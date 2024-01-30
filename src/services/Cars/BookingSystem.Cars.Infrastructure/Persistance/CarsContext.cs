using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Cars.Infrastructure.Persistance;

public class CarsContext(DbContextOptions<CarsContext> options) : DbContext(options)
{
}
