using EFcoreDemo.Interface;
using EFcoreDemo.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EFcoreDemo.CQRS.Commands.Delete
{
    public class DeleteBlogCommand(int BlogId) : IRequest<bool>
    {
        public int BlogId { get; } = BlogId;
    }

    public class DeleteBlogHandler : IRequestHandler<DeleteBlogCommand, bool>
    {
        private readonly IBlogRepository _blogRepository;

       public DeleteBlogHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<bool> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            return await _blogRepository.DeleteBlogAsync(request.BlogId, cancellationToken);
        }
    }
}
