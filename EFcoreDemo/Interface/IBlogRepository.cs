namespace EFcoreDemo.Interface
{
    public interface IBlogRepository
    {
        Task<int> InsertBlogReturnIdAsync(string url);
        Task<int> ModifyBlogAsync(int blxogId, string newUrl);
        Task<int> DeleteBlogAsync(int blogId);
    }

}