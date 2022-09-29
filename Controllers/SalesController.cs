using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stock_keeping.Data;
using Stock_keeping.Models;
using Stock_keeping.Utility;
using Stock_keeping.ViewModels;
using System.Security.Claims;

namespace Stock_keeping.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SalesController(ApplicationDbContext db)
        {
            _db = db;
        }

        private string GetOrg()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var orgId = _db.ApplicationUser.Where(c => c.Id == claim.Value).FirstOrDefault().OrgId;

            return orgId;
        }


        public async Task<IActionResult> AddSoldItems()
        {
            //RENDERING PRODUCT AND CUSTOMER AS DROPDOWNS INTO VIEW
            ViewData["Customer"] = new SelectList(await _db.Customer.Where(x => x.OrgId == GetOrg()).ToListAsync(), "Id", "CustomerName");
            ViewData["Product"] = new SelectList(await _db.Product.Where(x => x.OrgId == GetOrg()).ToListAsync(), "Id", "Name");
            var salesList = await _db.SalesList.Include(s => s.Product).Where(p => p.OrgId == GetOrg()).ToListAsync();
            ViewBag.salesList = salesList;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(SalesList salesData)
        {
            salesData.OrgId = GetOrg();
            _db.SalesList.Add(salesData);
            await _db.SaveChangesAsync();
            return Json(new
            {
                msg = "success"
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SalesList salesData)
        {
            salesData.OrgId = GetOrg();
            _db.Update(salesData);
            await _db.SaveChangesAsync();
            return Json(new
            {
                msg = "success"
            });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var salesItemtToDel = await _db.SalesList.FirstOrDefaultAsync(p => p.Id == id);
            _db.SalesList.Remove(salesItemtToDel);

            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });

        }

        [HttpPost]
        public async Task<IActionResult> CreatingReportAndSummary(SalesReportObj reportObj)
        {
            var stock = _db.SalesList.Where(p => p.OrgId == GetOrg()).ToList();
            foreach (var stockItem in stock)
            {
                var SalesReport = new SalesReport()
                {
                    PurchaseDate = DateTime.Parse(reportObj.PurchaseDate),
                    CustomerId = reportObj.CustomerId,
                    ProductId = stockItem.ProductId,
                    Quantity = stockItem.Quantity,
                    TotalCost = stockItem.Price,
                    OrgId = stockItem.OrgId
                };
                var inventory = await _db.StockList.Where(s => s.OrgId == GetOrg()).ToListAsync();
                foreach (var item in inventory)
                {
                    if (item.ProductId == stockItem.ProductId)
                    {
                        item.Sold +=stockItem.Quantity;
                        item.Quantity = item.Purchased -item.Sold;
                        
                        
                    }
                };

                _db.SalesReport.Add(SalesReport);
                await _db.SaveChangesAsync();
            }

            DateTime date = DateTime.Parse(reportObj.PurchaseDate);

            var SalesSummary = new SalesSummary()
            {
                PurchasedDate = date,
                CustomerId = reportObj.CustomerId,
                TotalCost = reportObj.TotalCost,
                OrgId = GetOrg()
            };

            _db.SalesSummary.Add(SalesSummary);
            await _db.SaveChangesAsync();

            _db.SalesList.RemoveRange(stock);
            await _db.SaveChangesAsync();
            return Json(new
            {
                msg = "success"
            });
        }

        [HttpGet]
        public IActionResult SolveTotalAmount()
        {
            var stock = _db.SalesList.Where(p => p.OrgId == GetOrg()).ToList();
            double amount = 0;
            double? discount = 0;
            double dicsCost = 0;
            double? tax = 0;
            double total = 0;
            foreach (var stockItem in stock)
            {
                amount += stockItem.Amount;
                discount += stockItem.DiscountPrice;
                dicsCost += stockItem.DiscountedCost;
                tax += stockItem.TaxPrice;
                total += stockItem.Price;

            };

            return Json(new
            {
                amount,
                discount,
                dicsCost,
                tax,
                total
            });
        }

        public async Task<IActionResult> SalesReport(SalesReportDateFilterVM model)
        {
            if (model.StartDate == null && model.EndDate == null)
            {
                model.EndDate = DateTime.Now;
                model.StartDate = DateTime.Now.AddDays(-30);
            }

            var salesReport = await _db.SalesReport.Include(p => p.Customer).Include(p => p.Product).Where(p => p.OrgId == GetOrg() && p.PurchaseDate >= model.StartDate && p.PurchaseDate <= model.EndDate).OrderBy(p => p.PurchaseDate).ToListAsync();
            model.SalesReports = salesReport;
            return View(model);
        }

        public async Task<IActionResult> SalesSummary(SalesSummaryDateFilterVM model)
        {
            if (model.StartDate == null && model.EndDate == null)
            {
                model.EndDate = DateTime.Now;
                model.StartDate = DateTime.Now.AddDays(-30);
            }

            var salesSummary = await _db.SalesSummary.Include(p => p.Customer).Where(p => p.OrgId == GetOrg() && p.PurchasedDate >= model.StartDate && p.PurchasedDate <= model.EndDate).OrderBy(p => p.PurchasedDate).ToListAsync();
            model.SalesSummaries = salesSummary;
            return View(model);
        }

    }
}
