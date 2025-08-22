using AutoMapper;
using EFcoreDemo.CQRS.Blogs.Command.Create;
using EFcoreDemo.CQRS.Blogs.Command.Delete;
using EFcoreDemo.CQRS.Blogs.Command.Update;
using EFcoreDemo.CQRS.Blogs.Queries.GetAll;
using EFcoreDemo.CQRS.Blogs.Queries.GetById;
using EFcoreDemo.Models.DataContext;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories.Interface;
using EFcoreDemo.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace EFcoreDemo.Controllers
{
    public class BlogController : Controller
    {
        private readonly DataContext _context;
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly BlogService _blogService;
        private readonly IMediator _mediator;
        public BlogController(DataContext context, IBlogRepository blogRepository,IMapper mapper, BlogService blogService, IMediator mediator)
        {
            _context = context;
            _blogRepository = blogRepository;
            _mapper = mapper;
            _blogService = blogService;
            _mediator = mediator;
        }
        #region
        /// <summary>
        // Retrieves all blog entries with their details.
        /// </summary>
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var blogs = await _mediator.Send(new GetAllBlogsQueryWithDetails(), cancellationToken);
            return View(blogs);
        }
        #endregion
        #region
        /// <summary>
        // Creates a new blog entry.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                await _mediator.Send(new CreateBlogCommand(blog.Url ?? string.Empty));
                return RedirectToAction("Index");
            }
            catch(InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(blog.Url), ex.Message);
                return View(blog);
            }
        }
        #endregion
        #region
        /// <summary>
        /// Retrieves a blog entry by its ID for editing.
        /// </summary>
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _mediator.Send(new GetBlogByIdCommand(id));
            return View(blog);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BlogViewModel blog)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();
                await _mediator.Send(new UpdateBlogCommand(blog.BlogId, blog.Url ?? string.Empty));
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(blog.Url), ex.Message);
                return View(blog);
            }
        }
        #endregion
        #region
        /// <summary>
        //Deletes a blog entry by its ID.
        /// </summary>
        public async Task<ActionResult> Delete(int id)
        {
            var blog = await _mediator.Send(new GetBlogByIdCommand(id));
            return View(blog);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveBlog(BlogViewModel bloged)
        {
            var result = await _mediator.Send(new DeleteBlogCommand(bloged.BlogId));
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region
        /// <summary>
        // Retrieves the details of a blog entry by its ID.
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _mediator.Send(new GetBlogByIdCommand(id));
            return View(blog);
        }
        #endregion
    }
}
