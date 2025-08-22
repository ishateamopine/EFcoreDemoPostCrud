using EFcoreDemo.CQRS.Common.Interface;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Blogs.Command.Update
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, bool>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogValidator _blogValidator;

        public UpdateBlogCommandHandler(IBlogRepository repository, IBlogValidator blogValidator)
        {
            _blogRepository = repository;
            _blogValidator = blogValidator;
        }

        #region
        /// <summary>
        // Updates an existing blog entry by its ID.
        /// </summary>
        public async Task<bool> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(request.BlogId, cancellationToken);
            if (blog == null) return false;

            var existblog = await _blogValidator.ValidateUpdateUrlAsync(request.BlogId, request.Url, cancellationToken);
            if(existblog != null)
            {
                throw new Exception($"Blog with URL '{request.Url}' already exists.");
            }

            blog.Url = request.Url;
            blog.UpdatedAt = DateTime.UtcNow;
            blog.UpdatedBy = "System";

            await _blogRepository.UpdateAsync(blog, cancellationToken);
            return true;
        }
        #endregion
    }
}
