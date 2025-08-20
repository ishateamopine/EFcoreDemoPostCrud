using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Blogs.Command.Update
{
    public class UpdateBlogCommand(int BlogId, string Url) : IRequest<bool>
    {
        public int BlogId { get; } = BlogId;
        public string Url { get; } = Url;
    }
}
