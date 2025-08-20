using EFcoreDemo.Interface;
using EFcoreDemo.Models;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.CQRS.Commands.Create;
using EFcoreDemo.CQRS.Commands.Delete;
using EFcoreDemo.CQRS.Commands.Select;
using EFcoreDemo.CQRS.Commands.Update;
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
            return await _mediator.Send(new GetBlogDetailsQuery(id));
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
