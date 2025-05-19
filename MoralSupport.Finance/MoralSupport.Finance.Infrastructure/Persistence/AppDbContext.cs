using Microsoft.EntityFrameworkCore;
using MoralSupport.Finance.Domain.Entities;

namespace MoralSupport.Finance.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<UserOrganization> UserOrganizations => Set<UserOrganization>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(255);

                entity.HasMany(u => u.UserOrganizations)
                      .WithOne(uo => uo.User)
                      .HasForeignKey(uo => uo.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Organization
            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.Name).IsRequired().HasMaxLength(255);
                entity.Property(o => o.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasMany(o => o.UserOrganizations)
                      .WithOne(uo => uo.Organization)
                      .HasForeignKey(uo => uo.OrganizationId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // UserOrganization
            modelBuilder.Entity<UserOrganization>(entity =>
            {
                entity.HasKey(uo => uo.Id);
                entity.Property(uo => uo.Role)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.HasIndex(uo => new { uo.UserId, uo.OrganizationId }).IsUnique();

            });
        }

    }
}
