
using System.ComponentModel.DataAnnotations;

namespace SupplierView.Models;

public class Package
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(0, 10000000)]
    public decimal Price { get; set; }

    public bool IsActive { get; set; } = true;
}
