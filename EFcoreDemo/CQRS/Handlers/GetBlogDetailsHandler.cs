using EFcoreDemo.CQRS.Queries;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EFcoreDemo.CQRS.Handlers
{
    public class GetBlogDetailsHandler : IRequestHandler<GetBlogDetailsQuery, BlogViewModel?>
    {
        private readonly DataContext _ctx;
    public GetBlogDetailsHandler(DataContext ctx) => _ctx = ctx;

    public async Task<BlogViewModel?> Handle(GetBlogDetailsQuery request, CancellationToken cancellationToken)
    {
        return await _ctx.Blogs
            .Select(x => new BlogViewModel
            {
                BlogId = x.BlogId,
                Url = x.Url!,
                PostCount = x.Posts.Count,
                RssUrl = null, 
                Posts = x.Posts.Select(p => new PostViewModel
                {
                    PostId = p.PostId,
                    Title = p.Title,
                    Content = p.Content
                }).ToList()
            })
            .FirstOrDefaultAsync(x => x.BlogId == request.BlogId, cancellationToken);
            }
    }
}
