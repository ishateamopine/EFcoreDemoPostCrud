using AutoMapper;
using EFcoreDemo.Interface;
using EFcoreDemo.Models.ViewModels;
using MediatR;

namespace EFcoreDemo.CQRS.Commands.Select
{
    public record GetPostQuery(int Id) : IRequest<PostViewModel>;
    public record GetAllPostsQuery() : IRequest<IEnumerable<PostViewModel>>;

    public class GetPostHandler : IRequestHandler<GetPostQuery, PostViewModel>,
                                  IRequestHandler<GetAllPostsQuery, IEnumerable<PostViewModel>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public GetPostHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PostViewModel> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<PostViewModel>(post);
        }

        public async Task<IEnumerable<PostViewModel>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PostViewModel>>(posts);
        }
    }
}
