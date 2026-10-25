
using System.ComponentModel.DataAnnotations;

namespace SupplierView.Models;

public class Booking
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string EventType { get; set; } = string.Empty;

    [Required]
    public DateTime EventDate { get; set; }

    [StringLength(100)]
    public string PackageName { get; set; } = string.Empty;

    [Range(0, 10000000)]
    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = "Pending";
}
