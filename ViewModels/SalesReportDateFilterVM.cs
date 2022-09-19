using Stock_keeping.Models;

namespace Stock_keeping.ViewModels
{
    public class SalesReportDateFilterVM
    {
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;

        public List<SalesReport> SalesReports { get; set; }
    }
}
