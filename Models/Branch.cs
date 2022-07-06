using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_keeping.Models
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BranchName { get; set; }
        [Required]
        public string BranchCode { get; set; }
        public string OrgId { get; set; }
    }
}
