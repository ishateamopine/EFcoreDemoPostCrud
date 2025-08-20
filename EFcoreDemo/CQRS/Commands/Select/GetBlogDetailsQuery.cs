using EFcoreDemo.Interface;
using EFcoreDemo.Models;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EFcoreDemo.CQRS.Commands.Select
{
    public class GetBlogDetailsQuery(int BlogId) : IRequest<BlogViewModel>
    {
        public int BlogId { get; } = BlogId;
    }
    public class GetBlogDetailsHandler : IRequestHandler<GetBlogDetailsQuery, BlogViewModel?>
    {
        private readonly IBlogRepository _blogRepository;

        public GetBlogDetailsHandler(IBlogRepository repository)
        {
            _blogRepository = repository;
        }
        public async Task<BlogViewModel?> Handle(GetBlogDetailsQuery request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(request.BlogId, cancellationToken);
            if (blog == null) return null;

            return new BlogViewModel
            {
                BlogId = blog.BlogId,
                Url = blog.Url,
                PostCount = blog.Posts?.Count ?? 0
            };
        }

    }
}
