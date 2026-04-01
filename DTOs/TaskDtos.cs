using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class CarCreateDto
    {
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

        public int? SellerUserId { get; set; }

        public List<int> CategoryIds { get; set; } = new();
    }

    public class CarUpdateDto
    {
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

        public bool IsAvailable { get; set; }

        public int? SellerUserId { get; set; }

        public List<int> CategoryIds { get; set; } = new();
    }

    public class CarAssignDto
    {
        [Required]
        public int SellerUserId { get; set; }
    }

    public class CarReadDto
    {
        public int Id { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Year { get; set; }
        public bool IsAvailable { get; set; }
        public int? SellerUserId { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public List<string> Categories { get; set; } = new();
    }
}
