using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stock_keeping.Data;
using Stock_keeping.Models;
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
                amount = Math.Round(amount,2),
                discount = Math.Round((decimal)discount, 2),
                dicsCost = Math.Round(dicsCost, 2),
                tax = Math.Round((decimal)tax,2),
                total = Math.Round(total, 2)
            });
        }

    }
}
