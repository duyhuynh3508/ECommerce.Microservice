using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Microservice.SharedLibrary.BaseEntity;

namespace ECommerce.Microservice.ProductService.Api.Entities
{
    public class BasketItem : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BasketItemID { get; set; }
        public int BasketID { get; set; }
        public int ProductID { get; set; }
        public int CurrencyID { get; set; }
        public int Quantity { get; set; }
        public decimal  PriceTotal { get; set; }
    }
}
