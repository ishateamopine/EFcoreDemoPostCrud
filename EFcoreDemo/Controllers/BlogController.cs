using AutoMapper;
using EFcoreDemo.CQRS.Commands;
using EFcoreDemo.CQRS.Queries;
using EFcoreDemo.Interface;
using EFcoreDemo.Models;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories;
using EFcoreDemo.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Reflection.Emit;
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
        // Post : /Blog/Index
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
                //<----------using repository pattern----------------->
                //int id= await _blogRepository.InsertBlogReturnIdAsync(url);

                //<----------using service pattern------------------>
                //int id = await _blogService.CreateBlogAsync(model);      

                //<----------using CQRS pattern------------------>
                int id = await _mediator.Send(new CreateBlogCommand(url));
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
            // Map Blog → BlogViewModel
            var viewModel = new BlogViewModel
            {
                BlogId = blog.BlogId,
                Url = blog.Url
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BlogViewModel blog) 
        {
            if (ModelState.IsValid)
            {
                //<----------using service pattern------------------>
                //await _blogService.UpdateBlogAsync(blog);

                //<----------using repository pattern----------------->
                //await _blogRepository.ModifyBlogAsync(blog.BlogId, blog.Url!);

                //<----------using CQRS pattern------------------>
                await _mediator.Send(new UpdateBlogCommand(blog.BlogId, blog.Url!));
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
            // Map Blog → BlogViewModel
            var viewModel = new BlogViewModel
            {
                BlogId = blog.BlogId,
                Url = blog.Url
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveBlog(BlogViewModel bloged)
        {
            await _blogService.DeletedBlogAsync(bloged.BlogId);
            return RedirectToAction(nameof(Index));
        }
        //Select....using cqrs......
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _mediator.Send(new GetBlogDetailsQuery(id));
            //var blog = await _context.Blogs.Include(b => b.Posts).FirstOrDefaultAsync(b => b.BlogId == id);

            if (blog == null)
            {
                return NotFound();
            }
            // Map Blog → BlogViewModel
            return View(blog);
        }      

    }
}
