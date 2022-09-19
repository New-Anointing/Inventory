using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock_keeping.Data;
using System.Security.Claims;

namespace Stock_keeping.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public InventoryController(ApplicationDbContext db)
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

        public async Task<IActionResult> StockList()
        {
            var StockList = await _db.StockList.Include(p => p.Product).Where(p => p.OrgId == GetOrg()).ToListAsync();
            ViewBag.StockList = StockList;
            return View();
        }
    }
}
