using EFcoreDemo.Models.DataContext;
using EFcoreDemo.Repositories.Interface;

namespace EFcoreDemo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public IBlogRepository Blogs { get; }
        public IPostRepository Posts { get; }

        public UnitOfWork(DataContext context,
                          IBlogRepository blogRepository,
                          IPostRepository postRepository)
        {
            _context = context;
            Blogs = blogRepository;
            Posts = postRepository;
        }

        public async Task<int> CompleteAsync() =>await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
