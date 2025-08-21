using EFcoreDemo.CQRS.Blogs.Command.Create;
using EFcoreDemo.CQRS.Common.Interface;
using EFcoreDemo.Models.DataContext;
using Microsoft.EntityFrameworkCore;

namespace EFcoreDemo.CQRS.Common
{
    public class BlogValidator : IBlogValidator
    {
        private readonly DataContext _context;
        public BlogValidator(DataContext context)
        {
            _context = context;
        }
        public async Task<string> ValidateAsync(CreateBlogCommand request)
        {
            if (await _context.Blogs.AnyAsync(x => x.Url == request.Url))
            {
                return "Duplicate Url found.";
            }
            return "Unique Url";
        }
    }
}
