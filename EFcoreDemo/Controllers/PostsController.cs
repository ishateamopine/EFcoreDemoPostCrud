using EFcoreDemo.CQRS.Posts.Command.Create;
using EFcoreDemo.CQRS.Posts.Command.Delete;
using EFcoreDemo.CQRS.Posts.Command.Update;
using EFcoreDemo.CQRS.Posts.Queries.GetAll;
using EFcoreDemo.CQRS.Posts.Queries.GetById;
using EFcoreDemo.Models.DataContext;
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
        #region
        /// <summary>
        // Retrieves all posts with their details.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var posts = await _mediator.Send(new GetAllPostsCommand());
            return View(posts);
        }
        #endregion
        #region
        /// <summary>
        // Creates a new post.  
        /// </summary>
        public IActionResult Create()
        {
            ViewBag.BlogId = new SelectList(_context.Blogs, "BlogId", "Url");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel postVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _mediator.Send(new CreatePostCommand(postVm));
                    return RedirectToAction(nameof(Index));
                }
                return View(postVm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(postVm.Title), ex.Message);
                return View(postVm);
            }
        }
        #endregion
        #region
        /// <summary>
        // Edits an existing post.
        /// </summary>
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _mediator.Send(new GetPostByIdCommand(id));
            if (post == null) return NotFound();
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _mediator.Send(new UpdatePostCommand(viewModel));
                    return RedirectToAction(nameof(Index));
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(viewModel.Title), ex.Message);
                return View(viewModel);
            }
        }
        #endregion
        #region
        /// <summary>
        // Deletes a post by its ID.
        /// </summary>
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _mediator.Send(new GetPostByIdCommand(id));
            if (post == null) return NotFound();
            return View(post);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new DeletePostCommand(id));
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var post = await _mediator.Send(new GetPostByIdCommand(id));
            return View(post);
        }
        #endregion
    }
}
