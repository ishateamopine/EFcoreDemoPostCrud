using AutoMapper;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Posts.Command.Create
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, bool>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request.Posts);
            return await _repository.InsertPostAsync(post) > 0;
        }
    }
}
