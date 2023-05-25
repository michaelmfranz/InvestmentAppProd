using InvestmentApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvestmentApp.Persistence
{
    public class OurDBContext : DbContext
    {
        public DbSet<Investment> Investments { get; set; }

        public OurDBContext(DbContextOptions<OurDBContext> options) : base(options)
        {
        }

    }
}
