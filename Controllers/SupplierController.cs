using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SupplierView.Data;
using SupplierView.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierView.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SupplierController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // =========================
        // DASHBOARD
        // =========================

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);

            var profile = await _context.SupplierProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            ViewBag.SupplierName = profile?.OwnerName
                ?? user?.UserName
                ?? "Supplier";

            ViewBag.TotalPackages = await _context.Packages.CountAsync();

            ViewBag.ActiveServices = await _context.Services
                .CountAsync(s => s.IsActive);

            ViewBag.TotalBookings = await _context.Bookings.CountAsync();

            ViewBag.TotalEarnings = await _context.Bookings
                .Where(b => b.Status == "Completed")
                .SumAsync(b => (decimal?)b.TotalPrice) ?? 0m;

            ViewBag.RecentBookings = await _context.Bookings
                .OrderByDescending(b => b.EventDate)
                .Take(5)
                .ToListAsync();

            return View("~/Views/Supplier/Index.cshtml");
        }

        // =========================
        // PACKAGES
        // =========================

        public async Task<IActionResult> Packages()
        {
            return View("~/Views/Supplier/Packages.cshtml",
                await _context.Packages.ToListAsync());
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
            if (!ModelState.IsValid)
                return View("~/Views/Supplier/CreatePackage.cshtml", package);

            _context.Packages.Add(package);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Packages));
        }

        [HttpGet]
        public async Task<IActionResult> EditPackage(int id)
        {
            var package = await _context.Packages.FindAsync(id);

            if (package == null)
                return NotFound();

            return View("~/Views/Supplier/EditPackage.cshtml", package);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPackage(int id, Package package)
        {
            if (id != package.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View("~/Views/Supplier/EditPackage.cshtml", package);

            var existing = await _context.Packages.FindAsync(id);

            if (existing == null)
                return NotFound();

            _context.Entry(existing).CurrentValues.SetValues(package);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Packages));
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

        // =========================
        // SERVICES
        // =========================

        public async Task<IActionResult> Services()
        {
            return View("~/Views/Supplier/Services.cshtml",
                await _context.Services.ToListAsync());
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
            if (!ModelState.IsValid)
                return View("~/Views/Supplier/CreateService.cshtml", service);

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Services));
        }

        [HttpGet]
        public async Task<IActionResult> EditService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
                return NotFound();

            return View("~/Views/Supplier/EditService.cshtml", service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(int id, Service service)
        {
            if (id != service.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View("~/Views/Supplier/EditService.cshtml", service);

            var existing = await _context.Services.FindAsync(id);

            if (existing == null)
                return NotFound();

            _context.Entry(existing).CurrentValues.SetValues(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Services));
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

        // =========================
        // BOOKINGS
        // =========================

        public async Task<IActionResult> Bookings()
        {
            var bookings = await _context.Bookings
                .OrderByDescending(b => b.EventDate)
                .ToListAsync();

            return View("~/Views/Supplier/Bookings.cshtml", bookings);
        }

        [HttpGet]
        public async Task<IActionResult> BookingDetails(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            return View("~/Views/Supplier/BookingDetails.cshtml", booking);
        }

        [HttpGet]
        public async Task<IActionResult> EditBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            return View("~/Views/Supplier/EditBooking.cshtml", booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBooking(int id, Booking booking)
        {
            if (id != booking.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View("~/Views/Supplier/EditBooking.cshtml", booking);

            var existing = await _context.Bookings.FindAsync(id);

            if (existing == null)
                return NotFound();

            _context.Entry(existing).CurrentValues.SetValues(booking);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Bookings));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBookingStatus(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            return View("~/Views/Supplier/UpdateBookingStatus.cshtml", booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBookingStatus(int id, string status)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            var allowedStatuses = new[]
            {
                "Pending",
                "Confirmed",
                "Completed",
                "Cancelled"
            };

            if (string.IsNullOrWhiteSpace(status) ||
                !allowedStatuses.Contains(status))
            {
                return BadRequest("Invalid booking status.");
            }

            booking.Status = status;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Bookings));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Bookings));
        }

        // =========================
        // CALENDAR
        // =========================

        public async Task<IActionResult> Calendar()
        {
            var bookings = await _context.Bookings
                .OrderBy(b => b.EventDate)
                .ToListAsync();

            return View("~/Views/Supplier/Calendar.cshtml", bookings);
        }

        // =========================
        // EARNINGS
        // =========================

        public async Task<IActionResult> Earnings()
        {
            var completedBookings = await _context.Bookings
                .Where(b => b.Status == "Completed")
                .OrderByDescending(b => b.EventDate)
                .ToListAsync();

            ViewBag.TotalEarnings = completedBookings.Sum(b => b.TotalPrice);
            ViewBag.CompletedCount = completedBookings.Count;

            ViewBag.PendingValue = await _context.Bookings
                .Where(b => b.Status == "Pending")
                .SumAsync(b => (decimal?)b.TotalPrice) ?? 0m;

            return View("~/Views/Supplier/Earnings.cshtml", completedBookings);
        }

        // =========================
        // PROFILE
        // =========================

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
                return Challenge();

            var profile = await _context.SupplierProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
            {
                var user = await _userManager.GetUserAsync(User);

                profile = new SupplierProfile
                {
                    UserId = userId,
                    OwnerName = user?.UserName ?? "",
                    EmailAddress = user?.Email ?? ""
                };
            }

            return View("~/Views/Supplier/Profile.cshtml", profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(SupplierProfile profile)
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
                return Challenge();

            // Always bind the profile to the currently logged-in user.
            ModelState.Remove(nameof(SupplierProfile.UserId));
            profile.UserId = userId;

            if (!ModelState.IsValid)
                return View("~/Views/Supplier/Profile.cshtml", profile);

            var existingProfile = await _context.SupplierProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (existingProfile == null)
            {
                profile.Id = 0;
                _context.SupplierProfiles.Add(profile);
            }
            else
            {
                existingProfile.BusinessName = profile.BusinessName;
                existingProfile.OwnerName = profile.OwnerName;
                existingProfile.EmailAddress = profile.EmailAddress;
                existingProfile.ContactNumber = profile.ContactNumber;
                existingProfile.BusinessAddress = profile.BusinessAddress;
                existingProfile.BusinessDescription = profile.BusinessDescription;
            }

            await _context.SaveChangesAsync();

            TempData["ProfileMessage"] = "Profile saved successfully!";

            return RedirectToAction(nameof(Profile));
        }
    }
}
