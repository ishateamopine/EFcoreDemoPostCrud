using EFcoreDemo.Models.DataContext;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EFcoreDemo.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DataContext _context;
        public BlogRepository(DataContext context) => _context = context;

        public async Task<int> InsertBlogAsync(Blog blog, CancellationToken cancellationToken = default)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync(cancellationToken);
            return blog.BlogId;
        }

        public async Task<bool> DeleteBlogAsync(int blogId, CancellationToken cancellationToken = default)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.BlogId == blogId, cancellationToken);
            if (blog == null) return false;

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task UpdateAsync(Blog blog, CancellationToken cancellationToken = default)
        {
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Blog> GetByIdAsync(int blogId, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Blogs
                .Include(b => b.Posts)
                .FirstOrDefaultAsync(b => b.BlogId == blogId, cancellationToken);
            return entity ?? new Blog();
        }
        public async Task<List<Blog>> GetAllAsync(CancellationToken cancellationToken = default)
        {
                return await _context.Blogs.Where(b => !b.IsDeleted).AsNoTracking().ToListAsync(cancellationToken);     
        }
        public async Task<bool> UrlExistsAsync(string url, CancellationToken cancellationToken = default)
        {
            return await _context.Blogs.AnyAsync(b => b.Url == url && !b.IsDeleted, cancellationToken);
        }

        public async Task<int> ModifyBlogAsync(int blogId, string newUrl)
        {
            return await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC dbo.modify_blog {blogId}, {newUrl}"
            );
        }

        public async Task<int> DeleteBlogAsync(int blogId)
        {
            return await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC dbo.delete_blog {blogId}"
            );
        }

        public async Task<int> InsertBlogReturnIdAsync(string url)
        {
            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.insert_blog";
            cmd.CommandType = CommandType.StoredProcedure;

            var pUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 4000) { Value = (object)url ?? DBNull.Value };
            var pOut = new SqlParameter("@NewId", SqlDbType.Int) { Direction = ParameterDirection.Output };

            cmd.Parameters.Add(pUrl);
            cmd.Parameters.Add(pOut);

            await cmd.ExecuteNonQueryAsync();

            return pOut.Value == DBNull.Value ? 0 : Convert.ToInt32(pOut.Value);
        }
    }
}
