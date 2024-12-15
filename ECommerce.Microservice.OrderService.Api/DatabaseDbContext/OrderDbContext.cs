using ECommerce.Microservice.OrderService.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Microservice.OrderService.Api.DatabaseDbContext
{
    public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("tblOrders");

                entity.HasOne<OrderStatus>()
                      .WithMany()
                      .HasForeignKey(o => o.OrderStatusID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("tblOrderItems");

                entity.HasOne<Order>()
                      .WithMany()
                      .HasForeignKey(o => o.OrderID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("tblOrderStatuses");

                entity.Property(o => o.OrderStatusID)
                .ValueGeneratedNever();
            });
        }
    }
}
