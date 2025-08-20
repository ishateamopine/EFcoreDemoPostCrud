using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Posts.Command.Delete
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
    {
        private readonly IPostRepository _repository;

        public DeletePostCommandHandler(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeletePostAsync(request.PostId) > 0;
        }
    }
}
