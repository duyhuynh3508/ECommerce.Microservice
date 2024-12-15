using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Microservice.SharedLibrary.BaseEntity;

namespace ECommerce.Microservice.OrderService.Api.Entities
{
    public class Order : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int OrderStatusID { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
