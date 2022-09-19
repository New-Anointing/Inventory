using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stock_keeping.Models;

namespace Stock_keeping.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Product> ProductNew { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<PurchaseList> PurchaseList { get; set; }
        public DbSet<PurchaseReport> PurchaseReport { get; set; }
        public DbSet<PurchaseSummary> PurchaseSummary { get; set; }
        public DbSet<SalesList> SalesList { get; set; }
        public DbSet<SalesReport> SalesReport { get; set; }
        public DbSet<SalesSummary> SalesSummary { get; set; }
        public DbSet<StockList> StockList { get; set; }
    }
}