using AutoMapper;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Blogs.Queries.GetById
{
    public class GetBlogByIdCommandHandler : IRequestHandler<GetBlogByIdCommand, BlogViewModel?>
    {
        private readonly IBlogRepository _repository;
        private readonly IMapper _mapper;

        public GetBlogByIdCommandHandler(IBlogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #region GetById
        /// <summary>
        // revieves a blog entry by its ID.
        /// </summary>
        public async Task<BlogViewModel?> Handle(GetBlogByIdCommand request, CancellationToken cancellationToken)
        {
            var blog = await _repository.GetByIdAsync(request.BlogId, cancellationToken);
            if (blog == null) return null;
            return _mapper.Map<BlogViewModel>(blog);
        }
        #endregion
    }
}
