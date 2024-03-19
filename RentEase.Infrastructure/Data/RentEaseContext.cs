using MassTransit;
using Microsoft.EntityFrameworkCore;
using RentEase.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace RentEase.Infrastructure.Data
{
    [ExcludeFromCodeCoverage]
    public class RentEaseContext : DbContext
    {
        public DbSet<Property> Properties { get; set; }

        public RentEaseContext(DbContextOptions<RentEaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}
