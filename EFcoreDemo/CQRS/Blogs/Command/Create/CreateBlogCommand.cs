using MediatR;

namespace EFcoreDemo.CQRS.Blogs.Command.Create
{
    public class CreateBlogCommand(string Url) : IRequest<int>
    {
        public string Url { get; } = Url;
    }
}
