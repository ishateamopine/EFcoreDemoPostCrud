using EFcoreDemo.Interface;
using EFcoreDemo.Models;
using EFcoreDemo.Models.Domain;
using MediatR;

namespace EFcoreDemo.CQRS.Commands.Create
{
    public class CreateBlogCommand(string Url) : IRequest<int>
    {
        public string Url { get; } = Url;
    }
    public class CreateBlogHandler : IRequestHandler<CreateBlogCommand, int>
    {
        private readonly IBlogRepository _blogRepository;

        public CreateBlogHandler(IBlogRepository blogRepository)
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
