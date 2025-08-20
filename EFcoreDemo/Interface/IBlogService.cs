using EFcoreDemo.Models.ViewModels;

namespace EFcoreDemo.Interface
{
    public interface IBlogService
    {
        Task<BlogViewModel?> GetByIdAsync(int id);
        Task<int> CreateAsync(string url);
        Task<bool> UpdateAsync(int id, string url);
        Task<bool> DeleteAsync(int id);
    }
}
