using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Posts.Command.Delete
{
    public record DeletePostCommand(int PostId) : IRequest<bool>;
}
