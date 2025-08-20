using AutoMapper;
using EFcoreDemo.Interface;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Models.ViewModels;
using MediatR;

namespace EFcoreDemo.CQRS.Commands.Create
{
    public record CreatePostCommand(PostViewModel Post) : IRequest<bool>;
    public class CreatePostHandler : IRequestHandler<CreatePostCommand, bool>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public CreatePostHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request.Post);
            return await _repository.InsertPostAsync(post) > 0;
        }
    }
}
