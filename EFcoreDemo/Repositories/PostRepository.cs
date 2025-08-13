using EFcoreDemo.Interface;
using EFcoreDemo.Models;
using EFcoreDemo.Models.ViewModels;
using System;

namespace EFcoreDemo.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;
        public PostRepository(DataContext context) => _context = context;

        public async Task AddAsync(Post post) => await _context.posts.AddAsync(post);

        public async Task<Post> GetByIdAsync(int id) => await _context.posts.FindAsync(id);
    }

}
