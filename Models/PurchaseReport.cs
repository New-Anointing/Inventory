using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_keeping.Models
{
    public class PurchaseReport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime PurchaseTime { get; set; }

        [Required]
        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }


        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double TotalCost { get; set; }
        public string OrgId { get; set; }


    }
}
