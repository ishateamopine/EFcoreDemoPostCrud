using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Blogs.Command.Create
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, int>
    {
        private readonly IBlogRepository _blogRepository;

        public CreateBlogCommandHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public async Task<int> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = new Blog
            {
                Url = request.Url,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            };
            return await _blogRepository.InsertBlogAsync(blog, cancellationToken);
        }
    }
}
