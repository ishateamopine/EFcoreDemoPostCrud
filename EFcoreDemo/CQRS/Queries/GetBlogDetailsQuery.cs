using EFcoreDemo.Models.ViewModels;
using MediatR;

namespace EFcoreDemo.CQRS.Queries
{
    public record GetBlogDetailsQuery(int BlogId) : IRequest<BlogViewModel>;
}
