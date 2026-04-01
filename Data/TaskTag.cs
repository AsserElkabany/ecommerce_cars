namespace WebApplication1.Data
{
    public class CarCategory
    {
        public int CarId { get; set; }
        public Car Car { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
