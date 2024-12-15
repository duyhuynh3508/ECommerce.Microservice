using ECommerce.Microservice.OrderService.Api.Models.Order;
using ECommerce.Microservice.SharedLibrary.BaseModel;

namespace ECommerce.Microservice.OrderService.Api.Models.OrderItem
{
    public class OrderItemModel : BaseModel
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}
