using ECommerce.Microservice.SharedLibrary.BaseModel;
namespace ECommerce.Microservice.ProductService.Api.Models.Category
{
    public class CategoryModel : BaseModel
    {
        public int CategoryID { get; set; }
        public required string CategoryName { get; set; }
    }
}
