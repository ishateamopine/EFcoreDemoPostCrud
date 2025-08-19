using EFcoreDemo.Models;
using EFcoreDemo.Models.Domain;

namespace EFcoreDemo.Interface
{
    public interface IBlogRepository
    {
        Task<int> InsertBlogAsync(Blog blog, CancellationToken cancellationToken);
        Task<Blog> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> DeleteBlogAsync(int blogId, CancellationToken cancellationToken);
        Task UpdateAsync(Blog blog, CancellationToken cancellationToken);
        //Store Procedure
        Task<int> InsertBlogReturnIdAsync(string url);
        Task<int> ModifyBlogAsync(int blxogId, string newUrl);

    }

}