using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Models;

namespace OnlineStore.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Review>().ToTable(review => review.HasCheckConstraint("CK_Review_Rating", "Rating >= 1 AND Rating <= 5"));

            modelBuilder.Entity<Product>().Property(product => product.CreatedAt).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<User>().Property(user => user.CreatedAt).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Review>().Property(review => review.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
