using Stock_keeping.Models;

namespace Stock_keeping.ViewModels
{
    public class PurchaseReportDateFilterVM
    {
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        
        public List<PurchaseReport> PurchaseReport { get; set; }
    }
}
