using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class ApplicationDB : DbContext
    {
        public ApplicationDB(DbContextOptions<ApplicationDB> options) : base(options)
        {
        }

        public DbSet<AppUser> users { get; set; } = default!;
        public DbSet<UserProfile> userProfiles { get; set; } = default!;
        public DbSet<Car> cars { get; set; } = default!;
        public DbSet<Category> categories { get; set; } = default!;
        public DbSet<CarCategory> carCategories { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().ToTable("users");
            modelBuilder.Entity<UserProfile>().ToTable("user_profiles");
            modelBuilder.Entity<Car>().ToTable("cars");
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<CarCategory>().ToTable("car_categories");

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.AppUser)
                .HasForeignKey<UserProfile>(p => p.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.ListedCars)
                .WithOne(c => c.Seller)
                .HasForeignKey(c => c.SellerUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<CarCategory>()
                .HasKey(cc => new { cc.CarId, cc.CategoryId });

            modelBuilder.Entity<CarCategory>()
                .HasOne(cc => cc.Car)
                .WithMany(c => c.CarCategories)
                .HasForeignKey(cc => cc.CarId);

            modelBuilder.Entity<CarCategory>()
                .HasOne(cc => cc.Category)
                .WithMany(c => c.CarCategories)
                .HasForeignKey(cc => cc.CategoryId);
        }
    }
}
