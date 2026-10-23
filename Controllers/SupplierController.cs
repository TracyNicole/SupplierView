
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplierView.Data;
using SupplierView.Models;

namespace SupplierView.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("~/Views/Supplier/Index.cshtml");
        }

        // PACKAGES

        public async Task<IActionResult> Packages()
        {
            var packages = await _context.Packages.ToListAsync();
            return View("~/Views/Supplier/Packages.cshtml", packages);
        }

        [HttpGet]
        public IActionResult CreatePackage()
        {
            return View("~/Views/Supplier/CreatePackage.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePackage(Package package)
        {
            if (ModelState.IsValid)
            {
                _context.Packages.Add(package);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Packages));
            }

            return View("~/Views/Supplier/CreatePackage.cshtml", package);
        }

        [HttpGet]
        public async Task<IActionResult> EditPackage(int id)
        {
            var package = await _context.Packages.FindAsync(id);

            if (package == null)
            {
                return NotFound();
            }

            return View("~/Views/Supplier/EditPackage.cshtml", package);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPackage(int id, Package package)
        {
            if (id != package.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Packages.Update(package);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Packages));
            }

            return View("~/Views/Supplier/EditPackage.cshtml", package);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePackage(int id)
        {
            var package = await _context.Packages.FindAsync(id);

            if (package != null)
            {
                _context.Packages.Remove(package);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Packages));
        }

        // SERVICES

        public async Task<IActionResult> Services()
        {
            var services = await _context.Services.ToListAsync();
            return View("~/Views/Supplier/Services.cshtml", services);
        }

        [HttpGet]
        public IActionResult CreateService()
        {
            return View("~/Views/Supplier/CreateService.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Services));
            }

            return View("~/Views/Supplier/CreateService.cshtml", service);
        }

        [HttpGet]
        public async Task<IActionResult> EditService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return View("~/Views/Supplier/EditService.cshtml", service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(int id, Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Services.Update(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Services));
            }

            return View("~/Views/Supplier/EditService.cshtml", service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Services));
        }
    }
}
