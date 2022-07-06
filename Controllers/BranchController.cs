using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock_keeping.Data;
using Stock_keeping.Models;
using System.Security.Claims;

namespace Stock_keeping.Controllers
{
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BranchController(ApplicationDbContext db)
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
            var Branch = await _db.Branch.Where(b=>b.OrgId == GetOrg()).ToListAsync();
            ViewBag.Branch = Branch;    
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Branch branch)
        {
            branch.OrgId = GetOrg();
            _db.Branch.Add(branch);
            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Branch branch)
        {
            branch.OrgId = GetOrg();
            _db.Update(branch);
            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var branchToDelete = await _db.Branch.FirstOrDefaultAsync(b => b.Id == id);
            _db.Branch.Remove(branchToDelete);
            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });
        } 
    }
}
