using Stock_keeping.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_keeping.Models
{
    public class PurchaseList : ITransactionInfo
    {

        public int Id { get ; set ; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        public int ProductId { get ; set ; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
        public int Quantity { get ; set ; }
        public double Amount { get ; set ; }
        public double? DiscountPrice { get ; set ; }
        public double DiscountedCost { get ; set ; }
        public double? TaxPrice { get ; set ; }
        public double Price { get ; set ; }
        public string OrgId { get ; set ; }
    }
}
