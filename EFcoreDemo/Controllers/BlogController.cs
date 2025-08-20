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
using System.Reflection.Metadata;
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
        // Post
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var blogs = await _mediator.Send(new GetAllBlogsQueryWithDetails(), cancellationToken);
            return View(blogs);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        public async Task<IActionResult> Create(string url,BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var BlogId = await _mediator.Send(new CreateBlogCommand(url));
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(model));
        }
        //Get
        public async Task<IActionResult> Edit(int id, string url)
        {
            var blog = await _mediator.Send(new GetBlogByIdCommand(id));
            if (blog == null) return NotFound();
            var command = new UpdateBlogCommand(blog.BlogId, blog.Url!);
            return View(command);
        }
        //Post
        [HttpPost]
        public async Task<IActionResult> Edit(BlogViewModel blog) 
        {
            if (ModelState.IsValid)
            {
                var command = new UpdateBlogCommand(blog.BlogId, blog.Url!);
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }
        //Get
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
        // GET
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _mediator.Send(new GetBlogByIdCommand(id));
            return View(blog);
        }      

    }
}
