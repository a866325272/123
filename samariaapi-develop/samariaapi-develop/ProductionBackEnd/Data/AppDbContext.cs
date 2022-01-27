using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductionBackEnd.Models.PlatformNews;
using ProductionBackEnd.Models.Products;
using ProductionBackEnd.Models.User;

namespace ProductionBackEnd.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string,
        IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PlatformNewsModel> PlatformNews { get; set; }
        public DbSet<ProductsModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.Entity<PlatformNewsModel>()
                .HasOne(x => x.CreateUser)
                .WithMany()
                .HasForeignKey(x => x.CreateUserId);

            builder.Entity<PlatformNewsModel>()
                .HasOne(x => x.UpdateUser)
                .WithMany()
                .HasForeignKey(x => x.UpdateUserId);

            builder.Entity<ProductsModel>()
                .HasOne(x => x.CreateUser)
                .WithMany()
                .HasForeignKey(x => x.CreateUserId);

            builder.Entity<ProductsModel>()
                .HasOne(x => x.UpdateUser)
                .WithMany()
                .HasForeignKey(x => x.UpdateUserId);
        }
    }
}
