using Stock_keeping.Models;

namespace Stock_keeping.ViewModels
{
    public class SalesSummaryDateFilterVM
    {
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;

        public List<SalesSummary> SalesSummaries { get; set; }
    }
}
