using System.ComponentModel.DataAnnotations;
using ECommerce.Microservice.SharedLibrary.BaseEntity;

namespace ECommerce.Microservice.ProductService.Api.Entities
{
    public class Category : BaseEntity
    {
        [Key]
        public int CategoryID { get; set; }
        public required string CategoryName { get; set; }
    }
}
