using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaCandidatoDotNet.Data;
using ProvaCandidatoDotNet.Models;

namespace ProvaCandidatoDotNet.Controllers
{
    public class TagController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Tag
        public async Task<IActionResult> Index()
        {
            var tags = await _context.Tags.AsNoTracking().OrderBy(t => t.Nome).ToListAsync();
            return View(tags);
        }

        // GET: /Tag/Create
        public IActionResult Create() => View();

        // POST: /Tag/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (!ModelState.IsValid)
                return View(tag);

            _context.Add(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Tag/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null) return NotFound();
            return View(tag);
        }

        // POST: /Tag/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Tag tag)
        {
            if (!ModelState.IsValid)
                return View(tag);

            _context.Update(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: /Tag/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
