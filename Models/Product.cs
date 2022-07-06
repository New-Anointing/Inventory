using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_keeping.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price should be greater than ${1}")]
        public double SellingPrice { get; set; }

        [Required]
        public double CostPrice { get; set; }

        public string? Description { get; set; }
        public string OrgId { get; set; }


    }
}
