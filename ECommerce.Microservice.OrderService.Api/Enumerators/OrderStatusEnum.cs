namespace ECommerce.Microservice.OrderService.Api.Enumerators
{
    public enum OrderStatusEnum : int
    {
        New = 1,        
        Processing = 2, 
        Shipped = 3,    
        Delivered = 4,  
        Completed = 5,  
        Canceled = 6,   
        Refunded = 7    
    }
}
