using EFcoreDemo.Models.DataContext;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EFcoreDemo.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DataContext _context;
        public BlogRepository(DataContext context) => _context = context;

        #region
        /// <summary>
        // Inserts a new blog entry into the database.
        /// </summary>
        public async Task<int> InsertBlogAsync(Blog blog, CancellationToken cancellationToken = default)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync(cancellationToken);
            return blog.BlogId;
        }
        #endregion
        #region
        /// <summary>
        // Deletes a blog entry from the database by its ID.
        /// </summary>
        public async Task<bool> DeleteBlogAsync(int blogId, CancellationToken cancellationToken = default)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.BlogId == blogId, cancellationToken);
            if (blog == null) return false;

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        #endregion
        #region
        /// <summary>
        // Updates an existing blog entry in the database.
        /// </summary>
        public async Task UpdateAsync(Blog blog, CancellationToken cancellationToken = default)
        {
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync(cancellationToken);
        }
        #endregion
        #region
        /// <summary>
        // Retrieves a blog entry by its ID, including its associated posts.
        /// </summary>
        public async Task<Blog> GetByIdAsync(int blogId, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Blogs
                .Include(b => b.Posts)
                .FirstOrDefaultAsync(b => b.BlogId == blogId, cancellationToken);
            return entity ?? new Blog();
        }
        #endregion
        #region
        /// <summary>
        // Retrieves all blog entries from the database.
        /// </summary>
        public async Task<List<Blog>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.Blogs.Where(b => !b.IsDeleted).AsNoTracking().ToListAsync(cancellationToken);
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }
        #endregion
        #region
        /// <summary>
        // Retrieves all blog entries with their associated posts.
        /// </summary>
        public async Task<int> ModifyBlogAsync(int blogId, string newUrl)
        {
            return await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.modify_blog {blogId}, {newUrl}"
            );
        }
        #endregion
        #region
        /// <summary>
        // Deletes a blog entry from the database by its ID using a stored procedure.
        public async Task<int> DeleteBlogReturnIdAsync(int blogId)
        {
            return await _context.Database.ExecuteSqlInterpolatedAsync( $"EXEC dbo.delete_blog {blogId}"
            );
        }
        #endregion
        #region
        /// <summary>
        // Inserts a new blog entry into the database using a stored procedure and returns the new blog ID.
        /// </summary>
        public async Task<int> InsertBlogReturnIdAsync(string url)
        {
           return await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.insert_blog {url}");
        }
        #endregion
    }
}
