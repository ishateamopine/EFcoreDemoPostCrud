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
        #region
        /// <summary>
        // Validates if a blog URL already exists in the database.
        /// </summary>
        public async Task ValidateDuplicateUrlAsync(string url, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Blogs.AnyAsync(x => x.Url == url, cancellationToken);
            if(exists)
            {
                throw new Exception($"Blog with URL '{url}' already exists.");
            }

        }
        #endregion
        #region
        /// <summary>
        // Validates if a blog URL already exists in the database for an update operation.
        /// </summary>
        public async Task<string> ValidateUpdateUrlAsync(int blogId, string url, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Blogs.AnyAsync(x => x.Url == url && x.BlogId != blogId, cancellationToken);
            if (exists)
            {
                return $"Blog with URL '{url}' already exists.";
            }
            return null;
        }
        #endregion
    }
}
