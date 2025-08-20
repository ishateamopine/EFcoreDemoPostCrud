using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Blogs.Command.Delete
{
    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, bool>
    {
        private readonly IBlogRepository _blogRepository;

        public DeleteBlogCommandHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<bool> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            return await _blogRepository.DeleteBlogAsync(request.BlogId, cancellationToken);
        }
    }
}
