using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Make { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Model { get; set; } = null!;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Year { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int? SellerUserId { get; set; }

        public AppUser? Seller { get; set; }

        public ICollection<CarCategory> CarCategories { get; set; } = new List<CarCategory>();
    }
}
