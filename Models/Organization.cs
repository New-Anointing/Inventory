using System.ComponentModel.DataAnnotations;

namespace Stock_keeping.Models
{
    public class Organization
    {
        [Key]
        public string Id { get; set; }
        public string OrganisationName { get; set; }
        public string OrganisationAddress { get; set; }
    }
}