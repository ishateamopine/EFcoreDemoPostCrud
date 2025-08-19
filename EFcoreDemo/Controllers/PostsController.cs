using EFcoreDemo.CQRS.Commands.Create;
using EFcoreDemo.CQRS.Commands.Delete;
using EFcoreDemo.CQRS.Commands.Select;
using EFcoreDemo.CQRS.Commands.Update;
using EFcoreDemo.Models;
using EFcoreDemo.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace EFcoreDemo.Controllers
{
    public class PostsController : Controller
    {
        private readonly DataContext _context;
        private readonly IMediator _mediator;

        public PostsController(DataContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            var posts = await _mediator.Send(new GetAllPostsQuery());
            return View(posts);
        }
        // GET
        public IActionResult Create()
        {
            ViewBag.BlogId = new SelectList(_context.Blogs, "BlogId", "Url");
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel postVm)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreatePostCommand(postVm));
                return RedirectToAction(nameof(Index));
            }
            return View(postVm);
        }


        // GET
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _mediator.Send(new GetPostQuery(id));
            if (post == null) return NotFound();
            return View(post);
        }

        // POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdatePostCommand(viewModel));
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
        
        //GET
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _mediator.Send(new GetPostQuery(id));
            if (post == null) return NotFound();
            return View(post);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new DeletePostCommand(id));
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var post = await _mediator.Send(new GetPostQuery(id));
            return View(post);
        }

    }
}
