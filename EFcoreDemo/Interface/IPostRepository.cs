using EFcoreDemo.Models.Domain;

namespace EFcoreDemo.Interface
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(int id);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<int> InsertPostAsync(Post post);
        Task<int> UpdatePostAsync(Post post);
        Task<int> DeletePostAsync(int id);
    }
}
