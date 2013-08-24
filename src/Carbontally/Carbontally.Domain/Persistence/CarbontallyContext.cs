using System.Data.Entity;
using Carbontally.Domain.Entities;

namespace Carbontally.Domain.Persistence
{
    public class CarbontallyContext : DbContext
    {
        // OnModelCreating goes here if you want to override it.

        public DbSet<Study> Studies { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }
    }
}
