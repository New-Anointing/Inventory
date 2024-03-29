﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stock_keeping.Data;
using Stock_keeping.Models;
using Stock_keeping.Utility;
using Stock_keeping.ViewModels;
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

        public async Task<IActionResult> AddNewPurchaseItem()
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
            var product = _db.Product.Where(p => p.Id == productId).Include(p => p.Category).FirstOrDefault();
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
                amount,
                discount,
                dicsCost,
                tax,
                total
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

        [HttpPost]
        public async Task<IActionResult> CreatingReportAndSummary(PurchaceReportObj reportObj)
        {
            var stock = _db.PurchaseList.Where(p => p.OrgId == GetOrg()).ToList();
            foreach (var stockItem in stock)
            {
                var PurReport = new PurchaseReport()
                {
                    PurchaseTime = DateTime.Parse(reportObj.PurchaseDate),
                    SupplierId = reportObj.SupplierId,
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
                        item.Purchased += stockItem.Quantity;
                        item.Quantity = item.Purchased - item.Sold;
                    }
                };
                _db.PurchaseReport.Add(PurReport);
                await _db.SaveChangesAsync();
            }

            DateTime date = DateTime.Parse(reportObj.PurchaseDate);

            var PurSummary = new PurchaseSummary()
            {
                PurchasedDate = date,
                SupplierId = reportObj.SupplierId,
                TotalCost = reportObj.TotalCost,
                OrgId = GetOrg()
            };

            _db.PurchaseSummary.Add(PurSummary);
            await _db.SaveChangesAsync();

            _db.PurchaseList.RemoveRange(stock);

            await _db.SaveChangesAsync();
            return Json(new
            {
                msg = "success"
            });
        }

        public async Task<IActionResult> PurchaseReport(PurchaseReportDateFilterVM model)
        {
            if (model.StartDate == null && model.EndDate == null)
            {
                model.EndDate = DateTime.Now;
                model.StartDate = DateTime.Now.AddDays(-30);
            }

            var purchaseReport = await _db.PurchaseReport.Include(p => p.Supplier).Include(p => p.Product).Where(p => p.OrgId == GetOrg() && p.PurchaseTime >= model.StartDate && p.PurchaseTime <= model.EndDate).OrderBy(p => p.PurchaseTime).ToListAsync();
            model.PurchaseReport = purchaseReport;
            return View(model);
        }

        public async Task<IActionResult> PurchaseSummary(PurchaseSummaryDateFilterVM model)
        {
            if (model.StartDate == null && model.EndDate == null)
            {
                model.EndDate = DateTime.Now;
                model.StartDate = DateTime.Now.AddDays(-30);
            }

            var purchaseSummary = await _db.PurchaseSummary.Include(p => p.Supplier).Where(p => p.OrgId == GetOrg() && p.PurchasedDate >= model.StartDate && p.PurchasedDate <= model.EndDate).OrderBy(p => p.PurchasedDate).ToListAsync();
            model.PurchaseSummary = purchaseSummary;
            return View(model);
        }
    }
}
