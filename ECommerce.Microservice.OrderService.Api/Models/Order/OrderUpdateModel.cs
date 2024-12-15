using ECommerce.Microservice.OrderService.Api.Enumerators;
using ECommerce.Microservice.SharedLibrary.BaseModel;

namespace ECommerce.Microservice.OrderService.Api.Models.Order
{
    public class OrderUpdateModel : BaseModel
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public OrderStatusEnum OrderStatusID { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
