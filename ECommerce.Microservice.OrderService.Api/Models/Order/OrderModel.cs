using ECommerce.Microservice.OrderService.Api.Enumerators;
using ECommerce.Microservice.OrderService.Api.Models.OrderItem;
using ECommerce.Microservice.SharedLibrary.BaseModel;

namespace ECommerce.Microservice.OrderService.Api.Models.Order
{
    public class OrderModel : BaseModel
    {
        public int OrderID { get; set; }
        public int UserID   { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public OrderStatusEnum OrderStatusID { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
