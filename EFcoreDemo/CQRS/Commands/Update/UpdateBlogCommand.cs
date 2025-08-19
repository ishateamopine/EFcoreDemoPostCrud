using EFcoreDemo.Interface;
using EFcoreDemo.Models;
using EFcoreDemo.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EFcoreDemo.CQRS.Commands.Update
{
    public class UpdateBlogCommand(int BlogId, string Url) : IRequest<bool>
    {
        public int BlogId { get; } = BlogId;
        public string Url { get; } = Url;
    }
    public class UpdateBlogHandler : IRequestHandler<UpdateBlogCommand, bool>
    {
        private readonly IBlogRepository _blogRepository;

        public UpdateBlogHandler(IBlogRepository repository)
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
