using ECommerce.Microservice.ProductService.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Microservice.ProductService.Api.DatabaseDbContext
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("tblProducts");

                entity.HasOne<Category>()
                    .WithMany()
                    .HasForeignKey(e => e.CategoryID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Currency>()
                    .WithMany()
                    .HasForeignKey(e => e.CurrencyID);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("tblCategories");
                entity.HasKey(e => e.CategoryID);

                entity.Property(e => e.CategoryID)
                      .ValueGeneratedNever();
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("tblCurrencies");
                entity.HasKey(e => e.CurrencyID);

                entity.Property(e => e.CurrencyID)
                      .ValueGeneratedNever();
            });
        }
    }
}
