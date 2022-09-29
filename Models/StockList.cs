using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_keeping.Models
{
    public class StockList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Purchased { get; set; }
        [Required]
        public int Sold { get; set; }
        public string OrgId { get; set; }

    }
}
