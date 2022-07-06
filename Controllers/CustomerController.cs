using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock_keeping.Data;
using Stock_keeping.Models;
using System.Security.Claims;

namespace Stock_keeping.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CustomerController(ApplicationDbContext db)
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
            var Customer = await _db.Customer.Where(c => c.OrgId == GetOrg()).ToListAsync();
            ViewBag.Customer = Customer;
            return View();
        }


        public async Task<IActionResult> Create(Customer customer)
        {
            customer.OrgId = GetOrg();
            _db.Customer.Add(customer);
            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });
        }

        public async Task<IActionResult> Edit(Customer customer)
        {
            customer.OrgId = GetOrg();
            _db.Update(customer);
            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customerToDelete = await _db.Customer.FirstOrDefaultAsync(c => c.Id == id);
            _db.Customer.Remove(customerToDelete);
            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });
        }
    }
}
