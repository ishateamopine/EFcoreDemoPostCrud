using EFcoreDemo.Interface;
using EFcoreDemo.Models;
using EFcoreDemo.Models.ViewModels;

namespace EFcoreDemo.Services
{
    public class BlogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // Create new Blog
        public async Task<int> CreateBlogAsync(BlogViewModel vm)
        {
            var blog = new Blog
            {
                Url = vm.Url
            };
            await _unitOfWork.Blogs.AddAsync(blog);
            await _unitOfWork.CompleteAsync();
            return blog.BlogId;
        }
        // Update existing Blog
        public async Task UpdateBlogAsync(BlogViewModel vm)
        {
            var blog = await _unitOfWork.Blogs.GetByIdAsync(vm.BlogId);
            if (blog == null)
                throw new Exception("Not found");

            blog.Url = vm.Url;
            await _unitOfWork.Blogs.UpdateAsync(blog);
            await _unitOfWork.CompleteAsync();
        }
        // Delete Blog
        public async Task<int> DeletedBlogAsync(int blogId)
        {
            int result = await _unitOfWork.Blogs.DeleteAsync(blogId);
            await _unitOfWork.CompleteAsync();
            return result;
        }

    }
}
