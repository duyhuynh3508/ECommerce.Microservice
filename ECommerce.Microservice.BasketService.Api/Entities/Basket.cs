using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Microservice.SharedLibrary.BaseEntity;

namespace ECommerce.Microservice.ProductService.Api.Entities
{
    public class Basket : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BasketID { get; set; }
        public int UserID { get; set; }
        public DateTime BasketCreatedOn { get; set; }
        public DateTime BasketLastUpdatedOn { get; set; }
    }
}
