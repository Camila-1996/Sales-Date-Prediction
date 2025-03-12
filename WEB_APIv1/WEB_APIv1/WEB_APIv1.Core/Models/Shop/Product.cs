
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_APIv1.Core.Models.Shop
{
    [Table("Products", Schema = "Production")]
    public class Product : BaseEntity
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(40)]
        public string ProductName { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; } = 0;

        [Required]
        public bool Discontinued { get; set; } = false;

        // Relaciones
        [ForeignKey("CategoryId")]
        public virtual ProductCategory Category { get; set; }

        //[ForeignKey("SupplierId")]
        //public virtual Supplier Supplier { get; set; }
    }
}
