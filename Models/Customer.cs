using System.ComponentModel.DataAnnotations;

namespace Stock_keeping.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }

        public string OrgId { get; set; }
    }
}
