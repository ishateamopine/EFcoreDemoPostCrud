using AutoMapper;
using EFcoreDemo.CQRS.Common.Interface;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Posts.Command.Update
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, bool>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPostValidator _postValidator;

        public UpdatePostCommandHandler(IPostRepository repository, IMapper mapper, IPostValidator postValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _postValidator = postValidator;
        }
        #region
        /// <summary>
        // Handles the update of an existing post entry.
        /// </summary>
        public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {        
            var post = _mapper.Map<Post>(request.posts);
            var validationMessage = await _postValidator.ValidateUpdateTitleAsync(request.posts.PostId, request.posts.Title);

            if (validationMessage != null)
            {
                throw new Exception(validationMessage);
            }
            return await _repository.UpdatePostAsync(post) > 0;
        }
        #endregion
    }
}
