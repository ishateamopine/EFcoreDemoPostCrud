using AutoMapper;
using EFcoreDemo.CQRS.Commands.Create;
using EFcoreDemo.CQRS.Commands.Delete;
using EFcoreDemo.CQRS.Commands.Select;
using EFcoreDemo.CQRS.Commands.Update;
using EFcoreDemo.Interface;
using EFcoreDemo.Models;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            var blogs = await _context.Blogs.Where(b => !b.IsDeleted).ToListAsync();
            var blogVMs = _mapper.Map<List<BlogViewModel>>(blogs);
            return View(blogVMs);
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
            return View("Create Data SuceessFully..!");
        }
        //Get
        public async Task<IActionResult> Edit(int id, string url)
        {
            var blog = await _mediator.Send(new GetBlogDetailsQuery(id));
            return View(blog);
        }
        //Post
        [HttpPost]
        public async Task<IActionResult> Edit(BlogViewModel blog) 
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new UpdateBlogCommand(blog.BlogId, blog.Url!));
                return RedirectToAction(nameof(Index));
            }
            return View(blog); 
        }
        //Get
        public async Task<ActionResult> Delete(int id)
        {
            var blog = await _mediator.Send(new GetBlogDetailsQuery(id));
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
            var blog = await _mediator.Send(new GetBlogDetailsQuery(id));
            return View(blog);
        }      

    }
}
