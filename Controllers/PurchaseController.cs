﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stock_keeping.Data;
using Stock_keeping.Models;
using System.Security.Claims;

namespace Stock_keeping.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PurchaseController(ApplicationDbContext db)
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
            //RENDERING CATEGORY PRODUCT AND SUPLIER AS DROPDOWNS INTO VIEW
            ViewData["Supplier"] = new SelectList(await _db.Supplier.Where(x => x.OrgId == GetOrg()).ToListAsync(), "Id", "SupplierName");
            ViewData["Product"] = new SelectList(await _db.Product.Where(x => x.OrgId == GetOrg()).ToListAsync(), "Id", "Name");
            var purchaseList = await _db.PurchaseList.Include(p => p.Category).Include(p => p.Product).Where(p => p.OrgId == GetOrg()).ToListAsync();
            ViewBag.purchaseList = purchaseList;
            return View();
        }

        [HttpGet]
        public IActionResult GetProductUsingSelectedProduct(int productId)
        {
            var product = _db.Product.Where(p=>p.Id == productId).Include(p=>p.Category).FirstOrDefault();
            return Json(new
            {
                data = product
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(PurchaseList purchaseData)
        {
            purchaseData.OrgId = GetOrg();
            _db.PurchaseList.Add(purchaseData);
            await _db.SaveChangesAsync();
            return Json(new
            {
                msg = "success"
            });
        }

        [HttpGet]
        public IActionResult SolveTotalAmount()
        {
            var stock = _db.PurchaseList.Where(p => p.OrgId == GetOrg()).ToList();
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
                amount = amount,
                discount = discount,
                dicsCost = dicsCost,
                tax = tax, 
                total = total
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PurchaseList purchaseData)
        {
            purchaseData.OrgId = GetOrg();
            _db.Update(purchaseData);
            await _db.SaveChangesAsync();
            return Json(new
            {
                msg = "success"
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var purchasedItemtToDel = await _db.PurchaseList.FirstOrDefaultAsync(p => p.Id == id);
            _db.PurchaseList.Remove(purchasedItemtToDel);

            await _db.SaveChangesAsync();

            return Json(new
            {
                msg = "success"
            });

        }
    }
}