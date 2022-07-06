using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock_keeping.Data;
using Stock_keeping.Models;
using System.Security.Claims;

namespace Stock_keeping.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SupplierController(ApplicationDbContext db)
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

        public async Task<IActionResult> Index()
        {
            var Supplier = await _db.Supplier.Where(s=>s.OrgId == GetOrg()).ToListAsync();
            ViewBag.Supplier = Supplier;
            return View();
        }


        // POST ACTION TO CREATE SUPPLIER
        [HttpPost]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            supplier.OrgId = GetOrg();
            _db.Supplier.Add(supplier);
            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });
        }
        
        // POST ACTION TO EDIT SUPPLIER
        [HttpPost]
        public async Task<IActionResult> Edit(Supplier supplier)
        {
            supplier.OrgId = GetOrg();
            _db.Update(supplier);
            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });
        }

        // POST ACTION TO DELETE SUPPLIER
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var supplierToDelete = await _db.Supplier.FirstOrDefaultAsync(s => s.Id == id);
            _db.Supplier.Remove(supplierToDelete);
            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });
        }
    }
}
