﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_keeping.Models
{
    public class SalesSummary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime PurchasedDate { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [Required]
        public double TotalCost { get; set; }

        public string OrgId { get; set; }

    }
}
