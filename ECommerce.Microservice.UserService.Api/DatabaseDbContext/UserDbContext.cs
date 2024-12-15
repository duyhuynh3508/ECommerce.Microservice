using ECommerce.Microservice.UserService.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Microservice.UserService.Api.DatabaseDbContext
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("tblUsers");

                entity.HasOne<UserRole>()
                .WithMany()
                .HasForeignKey(e => e.RoleID)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("tblUserRoles");

                entity.Property(e => e.RoleID)
                      .ValueGeneratedNever();
            });

        }
    }
}
