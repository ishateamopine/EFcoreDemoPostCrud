using AutoMapper;
using EFcoreDemo.CQRS.Common.Interface;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Posts.Command.Create
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, bool>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPostValidator _postValidator;

        public CreatePostCommandHandler(IPostRepository repository, IMapper mapper,IPostValidator postValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _postValidator = postValidator;
        }
        #region
        /// <summary>
        // Handles the creation of a new post entry.
        /// </summary>
        public async Task<bool> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            await _postValidator.ValidateDuplicateTitleAsync(request.posts.Title);
            var post = _mapper.Map<Post>(request.posts);
            return await _repository.InsertPostAsync(post) > 0;
        }
        #endregion
    }
}
