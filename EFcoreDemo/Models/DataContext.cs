using EFcoreDemo.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFcoreDemo.Models
{
    public class DataContext : DbContext
    {
        private readonly string _currentUser;
        public DataContext(DbContextOptions<DataContext> options, string ? currentUser = null) : base(options)
        {
            _currentUser = currentUser ?? "system";
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> posts { get; set; }
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Blog>() 
                .HasMany(b => b.Posts)
                .WithOne(p => p.Blog)
                .HasForeignKey(p => p.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            // Soft delete filter
            modelBuilder.Entity<Blog>().HasQueryFilter(b => !b.IsDeleted);
        }
    }
}
