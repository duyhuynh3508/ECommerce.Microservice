using System.ComponentModel.DataAnnotations;
using ECommerce.Microservice.SharedLibrary.BaseEntity;

namespace ECommerce.Microservice.ProductService.Api.Entities
{
    public class Currency : BaseEntity
    {
        [Key]
        public int CurrencyID { get; set; }
        public required string CurrencyName { get; set; }
    }
}
