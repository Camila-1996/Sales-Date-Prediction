
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_APIv1.Core.Models.Shop
{
    [Table("Categories", Schema = "Production")]
    public class ProductCategory : BaseEntity
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(15)]
        public string CategoryName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        // Relación con Product (una categoría puede tener muchos productos)
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
