using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class CategoryCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }

    public class CategoryReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
