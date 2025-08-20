using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Blogs.Command.Delete
{
    public class DeleteBlogCommand(int BlogId) : IRequest<bool>
    {
        public int BlogId { get; } = BlogId;
    }
}
