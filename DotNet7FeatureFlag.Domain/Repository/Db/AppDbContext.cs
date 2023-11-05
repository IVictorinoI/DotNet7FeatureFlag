using DotNet7FeatureFlag.App.Domain.Clients;
using DotNet7FeatureFlag.App.Repository.Clients;
using Microsoft.EntityFrameworkCore;

namespace DotNet7FeatureFlag.App.Repository.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClientConfig());
        }
    }
}
