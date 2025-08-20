using EFcoreDemo.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Commands.Delete
{
    public record DeletePostCommand(int PostId) : IRequest<bool>;
    public class DeletePostHandler : IRequestHandler<DeletePostCommand, bool>
    {
        private readonly IPostRepository _repository;

        public DeletePostHandler(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeletePostAsync(request.PostId) > 0;
        }
    }
}
