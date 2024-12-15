using ECommerce.Microservice.SharedLibrary.BaseModel;

namespace ECommerce.Microservice.OrderService.Api.Models
{
    public class OrderStatusModel : BaseModel
    {
        public int OrderStatusID { get; set; }
        public string OrderStatusName { get; set; }
    }
}
