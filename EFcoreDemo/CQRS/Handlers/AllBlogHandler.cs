using EFcoreDemo.CQRS.Commands;
using EFcoreDemo.Interface;
using EFcoreDemo.Models;
using EFcoreDemo.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EFcoreDemo.CQRS.Handlers
{
    public class AllBlogHandler : IRequestHandler<CreateBlogCommand, int>,
                                  IRequestHandler<UpdateBlogCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        private readonly DataContext _ctx;
        private readonly IBlogRepository _blogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AllBlogHandler(DataContext ctx, IBlogRepository blogRepository, IUnitOfWork unitOfWork,IUnitOfWork uow)
        {
            _ctx = ctx;
            _blogRepository = blogRepository;
            _unitOfWork = unitOfWork;
            _uow = uow;
        }

        public async Task<int> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = new Blog { Url = request.Url };
            await _uow.Blogs.AddAsync(blog);
            await _uow.CompleteAsync();
            return blog.BlogId;
        }
        public async Task<bool> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _uow.Blogs.GetByIdAsync(request.BlogId);
            if (blog == null) return false;

            blog.Url = request.Url;

            await _uow.CompleteAsync();
            return true;
        }

    }
}
