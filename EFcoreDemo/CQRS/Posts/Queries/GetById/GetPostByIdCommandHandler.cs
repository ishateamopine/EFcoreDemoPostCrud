using AutoMapper;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Posts.Queries.GetById
{
    public class GetPostByIdCommandHandler : IRequestHandler<GetPostByIdCommand, PostViewModel>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public GetPostByIdCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #region
        /// <summary>
        // Retrieves a post entry by its ID.
        /// </summary>
        public async Task<PostViewModel> Handle(GetPostByIdCommand request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetByIdAsync(request.PostId);
            return _mapper.Map<PostViewModel>(post);
        }
        #endregion
    }
}
