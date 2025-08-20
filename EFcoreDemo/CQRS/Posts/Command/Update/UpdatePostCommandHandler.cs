using AutoMapper;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Posts.Command.Update
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, bool>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public UpdatePostCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request.Posts);
            return await _repository.UpdatePostAsync(post) > 0;
        }
    }
}
