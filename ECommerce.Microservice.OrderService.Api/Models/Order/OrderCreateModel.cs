using ECommerce.Microservice.OrderService.Api.Enumerators;
using ECommerce.Microservice.SharedLibrary.BaseModel;

namespace ECommerce.Microservice.OrderService.Api.Models.Order
{
    public class OrderCreateModel : BaseModel
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
