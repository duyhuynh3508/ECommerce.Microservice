using ECommerce.Microservice.ProductService.Api.Enumerators;
using ECommerce.Microservice.SharedLibrary.BaseModel;

namespace ECommerce.Microservice.ProductService.Api.Models.Product
{
    public class ProductCreateModel : BaseModel
    {
        public CategoryEnum CategoryID { get; set; }
        public CurrencyEnum CurrencyID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }
}
