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
        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid)
                return View();

            await _mediator.Send(new CreateBlogCommand(blog.Url ?? string.Empty));
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(CreateBlogCommand command)
        //{
        //    if (!ModelState.IsValid)
        //        return View(command);

        //    await _mediator.Send(command);
        //    return RedirectToAction("Index");
        //}

        //Get
        public async Task<IActionResult> Edit(int id, string url, CancellationToken cancellationToken)
        {
            var blog = await _mediator.Send(new GetBlogByIdCommand(id),cancellationToken);
            return View(blog);
        }
        //Post
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBlogCommand command) 
        {
            try
            {
                var ok = await _mediator.Send(command);
                if (ok) return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", "Blog not found.");
                return View(command);
            }
            catch (FluentValidation.ValidationException ex)
            {
                foreach (var e in ex.Errors)
                    ModelState.AddModelError(e.PropertyName ?? nameof(command.Url), e.ErrorMessage);

                return View(command);
            }
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
