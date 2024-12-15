using ECommerce.Microservice.SharedLibrary.BaseModel;

namespace ECommerce.Microservice.OrderService.Api.Models.OrderItem
{
    public class OrderItemUpdateModel : BaseModel
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
