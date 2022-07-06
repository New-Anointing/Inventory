using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock_keeping.Data;
using Stock_keeping.Models;
using Stock_keeping.Utility;
using System.Security.Claims;

namespace Stock_keeping.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UsersController(ApplicationDbContext db)
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
            return View(await _db.ApplicationUser.Where(c => c.OrgId == GetOrg()).ToListAsync());
        }

        //LOCKING USERS
        public async Task<IActionResult> Lock(string Id)
        {
            if (Id==null)
            {
                return NotFound();
            }

            var applicationUser = await _db.ApplicationUser.Where(u=>u.Id == Id).FirstOrDefaultAsync();

            if(applicationUser == null)
            {
                return NotFound();
            }

            applicationUser.LockoutEnd = DateTime.Now.AddYears(1000);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //ULOCKING USERS
        public async Task<IActionResult> Unlock(string Id)
        {
            if (Id==null)
            {
                return NotFound();
            }

            var applicationUser = await _db.ApplicationUser.Where(u => u.Id == Id).FirstOrDefaultAsync();

            if (applicationUser == null)
            {
                return NotFound();
            }

            applicationUser.LockoutEnd = DateTime.Now;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
