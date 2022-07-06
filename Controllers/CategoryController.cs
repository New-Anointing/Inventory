using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock_keeping.Data;
using Stock_keeping.Models;
using System.Security.Claims;

namespace Stock_keeping.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
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
            var Category = await _db.Category.Where(c=>c.OrgId == GetOrg()).ToListAsync();
            ViewBag.Category = Category;
            return View();
        }



        // POST ACTION TO CREATE CATEGORY
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            category.OrgId = GetOrg();
            _db.Category.Add(category);
            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });
        }


        //POST ACTION TO EDIT CATEGORY
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            category.OrgId = GetOrg();
            _db.Update(category);
            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });

        }

        //DELETE ACTION TO DELETE CATEGORY
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var categoryToDelete = await _db.Category.FirstOrDefaultAsync(c => c.Id == id);

            _db.Category.Remove(categoryToDelete);
            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });
        }


    }
}
