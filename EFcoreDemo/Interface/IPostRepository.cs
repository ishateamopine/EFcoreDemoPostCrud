using EFcoreDemo.Models;

namespace EFcoreDemo.Interface
{
    public interface IPostRepository
    {
        Task AddAsync(Post post);
        Task<Post> GetByIdAsync(int id);
    }
}
