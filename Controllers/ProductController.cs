using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stock_keeping.Data;
using Stock_keeping.Models;
using Stock_keeping.Utility;
using System.Collections.Generic;
using System.Security.Claims;

namespace Stock_keeping.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
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
            //GETTING THE LIST OF CATEGORY IN JSON FORMAT
            ViewData["Category"] = new SelectList(await _db.Category.Where(x => x.OrgId == GetOrg()).ToListAsync(), "Id", "Name");
            var Products = await _db.Product.Include(p => p.Category).Where(p=>p.OrgId == GetOrg()).ToListAsync();
            ViewBag.Products = Products;
            return View();
        }

        //SAVING CREATED PRODUCT TO DB
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var stock = new StockList();
            product.OrgId = GetOrg();
            _db.Product.Add(product);
            await _db.SaveChangesAsync();
            var stockList = await _db.StockList.Include(p => p.Product).Where(p => p.OrgId == GetOrg()).ToListAsync();
            stock = new StockList
            {
                ProductId = product.Id,
                OrgId = GetOrg(),
                Quantity = 0
            };

            _db.StockList.Add(stock);
            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });
        }


        //SAVING EDITED PRODUCT TO DB
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            product.OrgId = GetOrg();
            _db.Update(product);
            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _db.Product.FirstOrDefaultAsync(p => p.Id == id);
            _db.Product.Remove(product);

            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });
            
        }
        

    }
}
