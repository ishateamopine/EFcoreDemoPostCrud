using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Blogs.Command.Update
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, bool>
    {
        private readonly IBlogRepository _blogRepository;

        public UpdateBlogCommandHandler(IBlogRepository repository)
        {
            _blogRepository = repository;
        }

        public async Task<bool> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(request.BlogId, cancellationToken);
            if (blog == null) return false;

            blog.Url = request.Url;
            blog.UpdatedAt = DateTime.UtcNow;
            blog.UpdatedBy = "System";

            await _blogRepository.UpdateAsync(blog, cancellationToken);
            return true;
        }

    }
}
