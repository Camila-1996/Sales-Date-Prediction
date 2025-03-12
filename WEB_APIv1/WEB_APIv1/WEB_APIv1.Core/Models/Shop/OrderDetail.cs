
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_APIv1.Core.Models.Shop
{
    [Table("OrderDetails", Schema = "Sales")]
    public class OrderDetail : BaseEntity
    {
        [Key, Column(Order = 2)]
        public int ProductId { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; } = 0;

        [Required]
        public short Qty { get; set; } = 1;

        [Required]
        [Column(TypeName = "numeric(4,3)")]
        public decimal Discount { get; set; } = 0;

        // Relaciones
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
