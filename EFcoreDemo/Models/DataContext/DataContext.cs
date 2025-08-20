using EFcoreDemo.Models.Common;
using EFcoreDemo.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFcoreDemo.Models.DataContext
{
    public class DataContext : DbContext
    {
        private readonly string _currentUser;
        public DataContext(DbContextOptions<DataContext> options, string? currentUser = null) : base(options)
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
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditBase>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "System";
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = "System";
                        break;

                    case EntityState.Deleted:
                        // Soft delete instead of hard delete
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                        entry.Entity.DeletedBy = "System";
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
