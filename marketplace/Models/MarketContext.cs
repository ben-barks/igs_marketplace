using System;
using Microsoft.EntityFrameworkCore;

namespace marketplace.Models
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions<MarketContext> options)
            : base(options)
        {
        }

        public DbSet<product> products { get; set; }
    }
}
