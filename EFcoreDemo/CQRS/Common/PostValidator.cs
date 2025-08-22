using EFcoreDemo.CQRS.Common.Interface;
using EFcoreDemo.Models.DataContext;
using Microsoft.EntityFrameworkCore;

namespace EFcoreDemo.CQRS.Common
{
    public class PostValidator : IPostValidator
    {
        private readonly DataContext _context;
        public PostValidator(DataContext context)
        {
            _context = context;
        }
        #region
        /// <summary>
        // Validates if a post title already exists in the database.
        /// </summary>
        public async Task ValidateDuplicateTitleAsync(string title)
        {
            var exists = await _context.posts.AnyAsync(x => x.Title == title);
            if (exists)
            {
                throw new Exception($"Post with title '{title}' already exists.");
            }
        }
        #endregion
        #region
        /// <summary>
        // Validates if a post title already exists in the database for an update operation.
        /// </summary>
        public async Task<string?> ValidateUpdateTitleAsync(int postId, string title)
        {
            var exists = await _context.posts.AnyAsync(x => x.Title == title && x.PostId != postId);
            if (exists != null)
            {
                return $"Post with title '{title}' already exists.";
            }
            else
            {
                return "Dublicate value allow";
            }
        }
        #endregion
    }
}
