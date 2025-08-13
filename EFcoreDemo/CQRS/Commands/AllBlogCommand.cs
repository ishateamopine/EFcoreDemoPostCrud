using EFcoreDemo.Models.ViewModels;
using MediatR;

namespace EFcoreDemo.CQRS.Commands
{
    public record CreateBlogCommand(string Url) : IRequest<int>;
    public record UpdateBlogCommand(int BlogId, string Url) : IRequest<bool>;
    //public record DeleteBlogCommand(int BlogId) : IRequest<bool>;
    //public record GetBlogDetailsQuery(int BlogId) : IRequest<BlogViewModel>;
}
