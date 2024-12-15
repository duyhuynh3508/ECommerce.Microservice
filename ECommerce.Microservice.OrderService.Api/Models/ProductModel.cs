namespace ECommerce.Microservice.OrderService.Api.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }
}
