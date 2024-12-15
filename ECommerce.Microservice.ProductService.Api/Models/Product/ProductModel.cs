using ECommerce.Microservice.SharedLibrary.BaseModel;
using ECommerce.Microservice.ProductService.Api.Enumerators;

namespace ECommerce.Microservice.ProductService.Api.Models.Product
{
    public class ProductModel : BaseModel
    {
        public int ProductID { get; set; }
        public CategoryEnum CategoryID { get; set; }
        public string CategoryName { get; set; }
        public CurrencyEnum CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }
}
