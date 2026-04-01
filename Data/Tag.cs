using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<CarCategory> CarCategories { get; set; } = new List<CarCategory>();
    }
}
