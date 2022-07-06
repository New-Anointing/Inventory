using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_keeping.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public string SupplierCode { get; set; }
        [Required]
        public string SupplierAddress { get; set; }
        [Required]
        public string SupplierPhonenumber { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        public string OrgId { get; set; }
    }
}
