using EFcoreDemo.CQRS.Blogs.Command.Create;

namespace EFcoreDemo.CQRS.Common.Interface
{
    public interface IBlogValidator
    {
        Task ValidateDuplicateUrlAsync(string url, CancellationToken cancellationToken = default);
        Task<string> ValidateUpdateUrlAsync(int blogId, string url, CancellationToken cancellationToken = default);
    }
}
