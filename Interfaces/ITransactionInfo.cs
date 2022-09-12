using Stock_keeping.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_keeping.Interfaces
{
    public interface ITransactionInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Amount { get; set; }

        [Range(1, int.MaxValue)]
        public double? DiscountPrice { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double DiscountedCost { get; set; }


        [Range(1, int.MaxValue)]
        public double? TaxPrice { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }
        public string OrgId { get; set; }
    }
}
