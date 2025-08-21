using EFcoreDemo.CQRS.Common.Interface;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Blogs.Command.Create
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, int>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogValidator _blogValidator;
        public CreateBlogCommandHandler(IBlogRepository blogRepository,IBlogValidator blogValidator)
        {
            _blogRepository = blogRepository;
            _blogValidator = blogValidator;
        }
        public async Task<int> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var result = await _blogValidator.ValidateAsync(request);


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
