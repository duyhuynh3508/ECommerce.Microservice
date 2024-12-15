using ECommerce.Microservice.ProductService.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Microservice.ProductService.Api.DatabaseDbContext
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>(entity =>
            {
                entity.ToTable("tblBackets");
            });

            modelBuilder.Entity<BasketItem>(entity =>
            {
                entity.ToTable("tblBasketItems");

                entity.HasOne<Basket>()
                .WithMany()
                .HasForeignKey(e => e.BasketID)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
