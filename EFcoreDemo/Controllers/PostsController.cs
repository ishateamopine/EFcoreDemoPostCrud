using EFcoreDemo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFcoreDemo.Models.ViewModels;


namespace EFcoreDemo.Controllers
{
    public class PostsController : Controller
    {
        private readonly DataContext _context;

        public PostsController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var posts = await _context.posts
                .Select(p => new PostViewModel
                {
                    PostId = p.PostId,
                    Title = p.Title,
                    Content = p.Content,
                    BlogId = p.BlogId
                }).ToListAsync();
            
            return View(posts);
        }

        public IActionResult Create()
        {
            ViewBag.BlogId = new SelectList(_context.Blogs, "BlogId", "Url");
            return View();
        }

        public IActionResult GetCreated()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post,PostViewModel post1)
        {
            if (ModelState.IsValid)
            {
                var postEntity = new PostViewModel
                {
                    Title = post.Title,
                    Content = post.Content,
                    BlogId = post.BlogId
                };

                _context.Add(postEntity);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }     
            return View(post1);
        }

        // GET: View for Editing a Post by id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var post = await _context.posts.FindAsync(id);
            if (post == null)
                return NotFound();

            var viewModel = new PostViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                BlogId = post.BlogId
            };

            return View(viewModel);
        }

        // POST : Edit a Post by id using Execute UpdateAsync method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostViewModel viewModel)
        {
            if (id != viewModel.PostId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var affected = await _context.posts.Where(p => p.PostId == id).ExecuteUpdateAsync(p => p
                        .SetProperty(p => p.Title, viewModel.Title)
                        .SetProperty(p => p.Content, viewModel.Content)
                        .SetProperty(p => p.BlogId, viewModel.BlogId)
                    );

                if (affected == 0)
                    return NotFound(); 

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        //GET : View for Details a Post by id
        public async Task<IActionResult> Details(int? id)
        {
            var post = await _context.posts.FirstOrDefaultAsync(p => p.PostId == id);

            if (post == null)
                return NotFound();
            // Map Post → PostViewModel
            var viewModel = new PostViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                BlogId = post.BlogId
            };

            return View(viewModel);
        }

        //GET : View for Deleting a Post by id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var post = await _context.posts.FindAsync(id);
            if (post == null)
                return NotFound();
            // Map Post → PostViewModel
            var viewModel = new PostViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                BlogId = post.BlogId
            };

            return View(viewModel);
        }

        // POST: Delete a post by id using Execute DeleteAsync method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int deletedRows = await _context.posts
                .Where(p => p.PostId == id)
                .ExecuteDeleteAsync();

            if (deletedRows == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
