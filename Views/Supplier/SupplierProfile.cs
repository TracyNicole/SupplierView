using System.ComponentModel.DataAnnotations;

namespace SupplierView.Models
{
    public class SupplierProfile
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string BusinessName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string OwnerName { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string EmailAddress { get; set; } = string.Empty;

        [Phone]
        [StringLength(30)]
        public string ContactNumber { get; set; } = string.Empty;

        [StringLength(250)]
        public string BusinessAddress { get; set; } = string.Empty;

        [StringLength(1000)]
        public string BusinessDescription { get; set; } = string.Empty;
    }
}
