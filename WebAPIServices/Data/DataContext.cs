global using Microsoft.EntityFrameworkCore;
using WebAPIServices.Models;

namespace WebAPIServices.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { 
        }

        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            //.OnDelete(DeleteBehavior.SetNull);
            //OnDelete(DeleteBehavior.Cascade)
        }
    }
}
