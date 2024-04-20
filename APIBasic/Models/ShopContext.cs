using Microsoft.EntityFrameworkCore;

namespace APIBasic.Models
{
    public class ShopContext: DbContext
    {
        public ShopContext(DbContextOptions options) : base(options) { }

        //db objects mapped from tables
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Seed();
        }


    }

    
}
