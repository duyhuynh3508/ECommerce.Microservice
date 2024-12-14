using ECommerce.Microservice.SharedLibrary.BaseModel;
namespace ECommerce.Microservice.ProductService.Api.Models.Currency
{
    public class CurrencyModel : BaseModel
    {
        public int CurrencyID { get; set; }
        public required string CurrencyName { get; set; }
    }
}
