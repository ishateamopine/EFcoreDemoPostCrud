using EFcoreDemo.Models;

namespace EFcoreDemo.Interface
{
    public interface IBlogRepository
    {
        Task AddAsync(Blog blog);
        Task<Blog> GetByIdAsync(int id);
        //Task<int> InsertBlogReturnIdAsync(string url);
        //Task<int> ModifyBlogAsync(int blxogId, string newUrl);
        Task<int> DeleteAsync(int blogId);
        Task UpdateAsync(Blog blog);


    }

}