using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WEB_APIv1.Core.Models.Account;

namespace WEB_APIv1.Core.Models.Shop
{
    [Table("Orders", Schema = "Sales")]
    public class Order : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public int? CustId { get; set; }

        [Required]
        public int EmpId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        [Required]
        public int ShipperId { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Freight { get; set; } = 0;

        [Required]
        [StringLength(40)]
        public string ShipName { get; set; }

        [Required]
        [StringLength(60)]
        public string ShipAddress { get; set; }

        [Required]
        [StringLength(15)]
        public string ShipCity { get; set; }

        [StringLength(15)]
        public string? ShipRegion { get; set; }

        [StringLength(10)]
        public string? ShipPostalCode { get; set; }

        [Required]
        [StringLength(15)]
        public string ShipCountry { get; set; }

        // Relaciones
        [ForeignKey("CustId")]
        public virtual Customer? Customer { get; set; }

        //[ForeignKey("EmpId")]
        //public virtual Employee Employee { get; set; }

        //[ForeignKey("ShipperId")]
        //public virtual Shipper Shipper { get; set; }
    }
}
