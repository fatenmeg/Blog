using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Identity;

namespace Blog.Controllers
{
    public class PostBlogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostBlogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PostBlogs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.postBlogs.Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PostBlogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postBlog = await _context.postBlogs
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postBlog == null)
            {
                return NotFound();
            }

            return View(postBlog);
        }

        // GET: PostBlogs/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tilte,SubTilte,Body,CreationTime,ImagePath,AuthorId")] PostBlog postBlog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postBlog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", postBlog.AuthorId);
            return View(postBlog);
        }

        // GET: PostBlogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postBlog = await _context.postBlogs.FindAsync(id);
            if (postBlog == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", postBlog.AuthorId);
            return View(postBlog);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tilte,SubTilte,Body,CreationTime,ImagePath,AuthorId")] PostBlog postBlog)
        {
            if (id != postBlog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postBlog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostBlogExists(postBlog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", postBlog.AuthorId);
            return View(postBlog);
        }

        // GET: PostBlogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postBlog = await _context.postBlogs
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postBlog == null)
            {
                return NotFound();
            }

            return View(postBlog);
        }

        // POST: PostBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postBlog = await _context.postBlogs.FindAsync(id);
            _context.postBlogs.Remove(postBlog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostBlogExists(int id)
        {
            return _context.postBlogs.Any(e => e.Id == id);
        }
    }
}
