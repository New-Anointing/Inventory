using Stock_keeping.Models;

namespace Stock_keeping.ViewModels
{
    public class PurchaseSummaryDateFilterVM
    {
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        
        public List<PurchaseSummary> PurchaseSummary { get; set; }
    }
}
