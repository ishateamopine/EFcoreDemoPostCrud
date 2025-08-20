using EFcoreDemo.Models.DataContext;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EFcoreDemo.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context) => _context = context;
  
        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.posts.Include(p => p.Blog).FirstOrDefaultAsync(p => p.PostId == id);
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.posts.Include(p => p.Blog).ToListAsync();
        }

        public async Task<int> InsertPostAsync(Post post)
        {
            _context.posts.Add(post);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdatePostAsync(Post post)
        {
            _context.posts.Update(post);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePostAsync(int id)
        {
            var post = await _context.posts.FindAsync(id);
            if (post != null)
            {
                _context.posts.Remove(post);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
