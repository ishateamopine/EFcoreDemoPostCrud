using AutoMapper;
using EFcoreDemo.Interface;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Models.ViewModels;
using MediatR;

namespace EFcoreDemo.CQRS.Commands.Update
{
    public record UpdatePostCommand(PostViewModel Post) : IRequest<bool>;
    public class UpdatePostHandler : IRequestHandler<UpdatePostCommand, bool>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public UpdatePostHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request.Post);
            return await _repository.UpdatePostAsync(post) > 0;
        }
    }
}
