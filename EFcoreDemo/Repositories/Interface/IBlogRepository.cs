using EFcoreDemo.Models.Domain;

namespace EFcoreDemo.Repositories.Interface
{
    public interface IBlogRepository
    {
        Task<int> InsertBlogAsync(Blog blog, CancellationToken cancellationToken);
        Task<Blog> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> DeleteBlogAsync(int blogId, CancellationToken cancellationToken);
        Task UpdateAsync(Blog blog, CancellationToken cancellationToken);
        Task<List<Blog>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> UrlExistsAsync(string url, CancellationToken cancellationToken = default);
        //Store Procedure
        Task<int> InsertBlogReturnIdAsync(string url);
        Task<int> ModifyBlogAsync(int blxogId, string newUrl);

    }

}