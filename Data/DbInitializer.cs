using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stock_keeping.Models;
using Stock_keeping.Utility;

namespace Stock_keeping.Data
{
    public class DbInitializer : IDbInitializer
    {

        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            ApplicationDbContext db,
            RoleManager<IdentityRole> roleManager)
        {
            _db=db;
            _roleManager=roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count()>0)
                {
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex)
            {

            }
            if (_db.Roles.Any(r => r.Name == SD.ManagerUser)) return;
            //ROLES CREATION
            _roleManager.CreateAsync(new IdentityRole(SD.ManagerUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.SalesOfficerUser)).GetAwaiter().GetResult();
        }
    }
}
