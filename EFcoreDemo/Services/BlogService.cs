using EFcoreDemo.CQRS.Blogs.Command.Create;
using EFcoreDemo.CQRS.Blogs.Command.Delete;
using EFcoreDemo.CQRS.Blogs.Command.Update;
using EFcoreDemo.CQRS.Blogs.Queries.GetById;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.Services
{
    public class BlogService : IBlogService
    {
        private readonly IMediator _mediator;

        public BlogService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<BlogViewModel?> GetByIdAsync(int id)
        {
            return await _mediator.Send(new GetBlogByIdCommand(id));
        }
        public async Task<int> CreateAsync(string url)
        {
            return await _mediator.Send(new CreateBlogCommand(url));
        }
        public async Task<bool> UpdateAsync(int id, string url)
        {
            return await _mediator.Send(new UpdateBlogCommand(id, url));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _mediator.Send(new DeleteBlogCommand(id));
        }

    }
}
