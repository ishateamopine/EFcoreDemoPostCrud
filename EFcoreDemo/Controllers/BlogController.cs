using AutoMapper;
using EFcoreDemo.Interface;
using EFcoreDemo.Models;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Reflection.Emit;
namespace EFcoreDemo.Controllers
{
    public class BlogController : Controller
    {
        private readonly DataContext _context;
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public BlogController(DataContext context, IBlogRepository blogRepository,IMapper mapper)
        {
            _context = context;
            _blogRepository = blogRepository;
            _mapper = mapper;
        }
        // Eager loading - includes Posts
        public async Task<IActionResult> Index()
        {
            var blogs = await _context.Blogs.Include(b => b.Posts).ToListAsync();
            var blogVMs = _mapper.Map<List<BlogViewModel>>(blogs);
            return View(blogVMs);
        }

        //Insert....
        public IActionResult Create()
        {
            return View();
        }     
        [HttpPost]
        public async Task<IActionResult> Create(string url,BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                int id= await _blogRepository.InsertBlogReturnIdAsync(url);
                TempData["Msg"] = $"Created id";             
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        //Update....
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
                return NotFound();

            return View(blog); 
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Blog blog) 
        {
            if (ModelState.IsValid)
            {
                await _blogRepository.ModifyBlogAsync(blog.BlogId, blog.Url!);
                return RedirectToAction(nameof(Index));
            }

            return View(blog); 
        }
        //Delete....
        public async Task<ActionResult> Delete(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
                return NotFound();
            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveBlog(Blog bloged)
        {
            await _blogRepository.DeleteBlogAsync(bloged.BlogId);
            return RedirectToAction(nameof(Index));
        }
        //Select....
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _context.Blogs.Include(b => b.Posts).FirstOrDefaultAsync(b => b.BlogId == id);

            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }      

    }
}
