using Microsoft.EntityFrameworkCore;
using MyFirstMVCAPP.Models;

namespace MyFirstMVCAPP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                  new Category() { Id = 1, Name = "Select Category" },
                  new Category() { Id = 2, Name = "Computers" },
                  new Category() { Id = 3, Name = "Mobiles" },
                  new Category() { Id = 4, Name = "Electric machines" },
                  new Category() { Id = 5, Name = "Other" }
                );
            modelBuilder.Entity<Item>()
            .HasOne(i => i.Category)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CategoryId)
            .HasConstraintName("FK_Items_Categories_CategoryId")
            .OnDelete(DeleteBehavior.Restrict); // Optional, or .Cascade/.SetNull etc.

            base.OnModelCreating(modelBuilder);
        }
    }
}
