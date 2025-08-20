using AutoMapper;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Posts.Queries.GetAll
{
    public class GetAllPostsCommandHandler : IRequestHandler<GetAllPostsCommand, IEnumerable<PostViewModel>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public GetAllPostsCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostViewModel>> Handle(GetAllPostsCommand request, CancellationToken cancellationToken)
        {
            var posts = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PostViewModel>>(posts);
        }
    }
}
