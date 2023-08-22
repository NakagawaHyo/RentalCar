using Microsoft.EntityFrameworkCore;
using RentalCar.Application.Models;
using System.Reflection.Emit;

namespace RentalCar.Application.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                @"Host=localhost;Database=RentalCar;Username=postgres;Password=asdf1234;Persist Security Info=True");
        }

        // ユーザー情報
        public virtual DbSet<User> Users { set; get; }
        public virtual DbSet<Role> Roles { set; get; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        // 車情報
        public virtual DbSet<CarClass> CarClasses { get; set; }
        public virtual DbSet<CarCategory> CarCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // it should be placed here, otherwise it will rewrite the following settings!
            base.OnModelCreating(builder);

            // Custom application mappings
            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(450);
                entity.HasIndex(e => e.LoginId).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
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

            builder.Entity<CarClass>().Navigation(e => e.carCategory).AutoInclude();
        }
    }
}
