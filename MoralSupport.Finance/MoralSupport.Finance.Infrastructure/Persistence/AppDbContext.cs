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
        public DbSet<AccountType> AccountTypes => Set<AccountType>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<Payee> Payees => Set<Payee>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Property> Properties => Set<Property>();


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

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.HasKey(at => at.Id);
                entity.Property(at => at.TypeName).IsRequired().HasMaxLength(100);

                entity.HasData(
                [
                    new AccountType { Id = 1, TypeName = "Checking" },
                    new AccountType { Id = 2, TypeName = "Savings" },
                    new AccountType { Id = 3, TypeName = "Credit Card" },
                    new AccountType { Id = 4, TypeName = "Investment" },
                    new AccountType { Id = 5, TypeName = "Loan" }
                ]);
            });
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.AccountName).IsRequired().HasMaxLength(255);
                entity.Property(a => a.Balance).HasColumnType("decimal(18,2)").HasDefaultValue(0m);
                entity.HasOne(a => a.Organization)
                      .WithMany()
                      .HasForeignKey(a => a.OrganizatoinId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.User)
                      .WithMany()
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.AccountType)
                      .WithMany(at => at.Accounts)
                      .HasForeignKey(a => a.AccountTypeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Payee>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.PayeeName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.Address).HasMaxLength(255);
                entity.HasOne(p => p.Organization).WithMany().HasForeignKey(p => p.OrganizationId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.CategoryName).IsRequired().HasMaxLength(255);
                entity.HasOne(c => c.Organization).WithMany().HasForeignKey(c => c.OrganizationId);
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.PropertyName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.Address).HasMaxLength(255);
                entity.Property(p => p.Notes).HasMaxLength(500);
                entity.HasOne(p => p.Organization).WithMany().HasForeignKey(p => p.OrganizationId);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Amount)
                      .HasColumnType("decimal(18,2)");

                entity.Property(t => t.Description)
                      .HasMaxLength(500);

                entity.Property(t => t.TransactionDate)
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(t => t.Account)
                      .WithMany()
                      .HasForeignKey(t => t.AccountId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Payee)
                      .WithMany()
                      .HasForeignKey(t => t.PayeeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Property)
                      .WithMany()
                      .HasForeignKey(t => t.PropertyId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Category)
                      .WithMany(c => c.Transactions)
                      .HasForeignKey(t => t.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
