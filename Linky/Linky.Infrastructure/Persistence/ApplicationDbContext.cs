using Linky.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Linky.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<UrlRecord> UrlRecords { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrlRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OriginalUrl).IsRequired().HasMaxLength(2048);
            entity.Property(e => e.ShortCode).IsRequired().HasMaxLength(10);
            entity.HasIndex(e => e.ShortCode).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}
