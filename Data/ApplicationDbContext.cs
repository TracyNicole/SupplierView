
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupplierView.Models;

namespace SupplierView.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext(options)
{
    public DbSet<Package> Packages { get; set; }

    public DbSet<Service> Services { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    public DbSet<SupplierProfile> SupplierProfiles { get; set; }
}
