using EFcoreDemo.Interface;
using EFcoreDemo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EFcoreDemo.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DataContext _context;
        public BlogRepository(DataContext context) => _context = context;
        public async Task AddAsync(Blog blog) => await _context.Blogs.AddAsync(blog);
        public async Task UpdateAsync(Blog blog) => _context.Blogs.Update(blog);
        public async Task<int> DeleteAsync(int blogId)
        {
            return await _context.Blogs
                .Where(b => b.BlogId == blogId)
                .ExecuteDeleteAsync();
        }


        public async Task<Blog> GetByIdAsync(int id) => await _context.Blogs.FindAsync(id);


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
