using System.Reflection.Emit;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RentalCar.Models;

namespace RentalCar.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            ChangeTracker.StateChanged += UpdateTimestamps;
            ChangeTracker.Tracked += UpdateTimestamps;
        }

        private static void UpdateTimestamps(object? sender, EntityEntryEventArgs e)
        {
            if (e.Entry.Entity is IHasTimestamps entityWithTimestamps)
            {
                switch (e.Entry.State)
                {
                    case EntityState.Modified:
                        entityWithTimestamps.UpdatedAt = DateTime.Now;
                        var databaseValues = e.Entry.GetDatabaseValues();
                        if (databaseValues != null)
                        {
                            entityWithTimestamps.CreatedAt = databaseValues.GetValue<DateTime?>("CreatedAt");
                        }
                        break;
                    case EntityState.Added:
                        entityWithTimestamps.UpdatedAt = DateTime.Now;
                        entityWithTimestamps.CreatedAt = DateTime.Now;
                        break;
                }
            }
        }

        public virtual DbSet<User> Users { set; get; }
        public virtual DbSet<Role> Roles { set; get; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        public virtual DbSet<Store> Stores { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // it should be placed here, otherwise it will rewrite the following settings!
            base.OnModelCreating(builder);

            // Custom application mappings
            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(450).IsRequired();
                entity.Property(e => e.Password).IsRequired();
            });

            builder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(450).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
            });

            builder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.RoleId);
                entity.Property(e => e.UserId);
                entity.Property(e => e.RoleId);
                entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasForeignKey(d => d.RoleId);
                entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasForeignKey(d => d.UserId);
            });

            builder.Entity<Role>().HasData(
                new Role { Id = 1, Name = CustomRoles.User },
                new Role { Id = 2, Name = CustomRoles.Admin }
            );

            builder.Entity<User>()
                .HasOne(e => e.Store)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.StoreId)
                .IsRequired();

            builder.Entity<Store>().HasData(
                new Store { Id = 1, Name = "管理者", Description="管理拠点", ZipCode= "100-8929", Address1= "東京都千代田区霞が関２丁目１−１"}
            );

            builder.Entity<User>().HasData(
                new User { Id = 1, Name = "管理者", Email = "admin@admin.com", LoginId = "admin", Password = "admin", StoreId = 1}
            );
        }
    }
}
