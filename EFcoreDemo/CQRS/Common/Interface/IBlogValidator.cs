using EFcoreDemo.CQRS.Blogs.Command.Create;

namespace EFcoreDemo.CQRS.Common.Interface
{
    public interface IBlogValidator
    {
        Task<string> ValidateAsync(CreateBlogCommand request);
    }
}
