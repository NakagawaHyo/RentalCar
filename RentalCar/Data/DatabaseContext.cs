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

        public virtual DbSet<CarCategory> CarCategories { set; get; }

        public virtual DbSet<CarClass> CarClasses { set; get; }

        public virtual DbSet<CarType> CarTypes { set; get; }

        public virtual DbSet<CarDivision> CarDivisions { set; get; }

        public virtual DbSet<Car> Cars { set; get; }

        public virtual DbSet<Client> Clients { set; get; }

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
                new Store { Id = 1, Name = "管理拠点", Description="管理拠点", ZipCode= "100-8929", Address1= "東京都千代田区霞が関２丁目１−１"}
            );

            builder.Entity<User>().HasData(
                new User { Id = 1, Name = "管理者", Email = "admin@admin.com", LoginId = "admin", Password = "admin", StoreId = 1}
            );

            builder.Entity<CarClass>()
                .HasOne(e => e.CarCategory)
                .WithMany(e => e.CarClasses)
                .HasForeignKey(e => e.CarCategoryId)
                .IsRequired();

            builder.Entity<CarCategory>().HasData(
               new CarCategory { Id = 1, Name = "乗用車" },
               new CarCategory { Id = 2, Name = "軽自動車" },
               new CarCategory { Id = 3, Name = "エコカー" },
               new CarCategory { Id = 4, Name = "スポーツカー" },
               new CarCategory { Id = 5, Name = "ミニバン・ワゴン" },
               new CarCategory { Id = 6, Name = "SUV" },
               new CarCategory { Id = 7, Name = "バン" },
               new CarCategory { Id = 8, Name = "トラック" },
               new CarCategory { Id = 9, Name = "バス" },
               new CarCategory { Id = 10, Name = "その他" }
           );

            builder.Entity<CarDivision>().HasData(
               new CarDivision { Id = 1, Name = "乗用車" },
               new CarDivision { Id = 2, Name = "マイクロバス" },
               new CarDivision { Id = 3, Name = "貨物自動車" },
               new CarDivision { Id = 4, Name = "特種用途車" },
               new CarDivision { Id = 5, Name = "二輪車" }
           );

            builder.Entity<CarClass>(entity =>
            {
                entity.Navigation(m => m.CarCategory).AutoInclude();
            });

            builder.Entity<CarType>(entity =>
            {
                entity.Navigation(m => m.CarClass).AutoInclude();
            });

            builder.Entity<Car>(entity =>
            {
                entity.Navigation(m => m.CarClass).AutoInclude();
                entity.Navigation(m => m.CarDivision).AutoInclude();
                entity.Navigation(m => m.CarType).AutoInclude();
                entity.Navigation(m => m.Store).AutoInclude();
            });
        }
    }
}
