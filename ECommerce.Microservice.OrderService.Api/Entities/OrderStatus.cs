using System.ComponentModel.DataAnnotations;
using ECommerce.Microservice.SharedLibrary.BaseEntity;

namespace ECommerce.Microservice.OrderService.Api.Entities
{
    public class OrderStatus : BaseEntity
    {
        [Key]
        public int OrderStatusID { get; set; }
        public string OrderStatusName { get; set; } = string.Empty;
    }
}
