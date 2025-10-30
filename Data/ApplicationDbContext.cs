using CarDealerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealerApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Inquiry> Inquiries => Set<Inquiry>();
    }
}
