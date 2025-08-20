namespace EFcoreDemo.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IBlogRepository Blogs { get; }
        IPostRepository Posts { get; }
        Task<int> CompleteAsync();

    }

}
