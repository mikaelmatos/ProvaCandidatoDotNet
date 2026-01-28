using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaCandidatoDotNet.Data;
using ProvaCandidatoDotNet.Models;

namespace ProvaCandidatoDotNet.Controllers
{
    public class NoticiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoticiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Noticia
        public async Task<IActionResult> Index()
        {
            var noticias = await _context.Noticias
                .Include(n => n.NoticiaTags)
                .ThenInclude(nt => nt.Tag)
                .OrderByDescending(n => n.DataPublicacao)
                .AsNoTracking()
                .ToListAsync();

            return View(noticias);
        }

        // GET: /Noticia/Create (Ajax)
        public async Task<IActionResult> Create()
        {
            var vm = new NoticiaViewModel
            {
                TagsDisponiveis = await _context.Tags.OrderBy(t => t.Nome).ToListAsync()
            };
            return PartialView("_NoticiaFormPartial", vm);
        }

        // GET: /Noticia/Edit/5 (Ajax)
        public async Task<IActionResult> Edit(int id)
        {
            var noticia = await _context.Noticias
                .Include(n => n.NoticiaTags)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (noticia == null) return NotFound();

            var vm = new NoticiaViewModel
            {
                Id = noticia.Id,
                Titulo = noticia.Titulo,
                Conteudo = noticia.Conteudo,
                DataPublicacao = noticia.DataPublicacao,
                TagsDisponiveis = await _context.Tags.OrderBy(t => t.Nome).ToListAsync(),
                SelectedTagIds = noticia.NoticiaTags.Select(nt => nt.TagId).ToList()
            };

            return PartialView("_NoticiaFormPartial", vm);
        }

        // POST: /Noticia/Save (Ajax)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(NoticiaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.TagsDisponiveis = await _context.Tags.OrderBy(t => t.Nome).ToListAsync();
                return PartialView("_NoticiaFormPartial", vm);
            }

            Noticia noticia;

            if (vm.Id == 0)
            {
                noticia = new Noticia
                {
                    Titulo = vm.Titulo,
                    Conteudo = vm.Conteudo,
                    DataPublicacao = vm.DataPublicacao
                };

                _context.Noticias.Add(noticia);
            }
            else
            {
                noticia = await _context.Noticias
                    .Include(n => n.NoticiaTags)
                    .FirstOrDefaultAsync(n => n.Id == vm.Id);

                if (noticia == null) return NotFound();

                noticia.Titulo = vm.Titulo;
                noticia.Conteudo = vm.Conteudo;
                noticia.DataPublicacao = vm.DataPublicacao;

                // Remove vínculos antigos
                _context.NoticiaTags.RemoveRange(noticia.NoticiaTags);
                noticia.NoticiaTags.Clear();
            }

            // Adiciona novas tags
            if (vm.SelectedTagIds?.Any() == true)
            {
                foreach (var tagId in vm.SelectedTagIds.Distinct())
                {
                    noticia.NoticiaTags.Add(new NoticiaTag { TagId = tagId, Noticia = noticia });
                }
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // POST: /Noticia/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var noticia = await _context.Noticias.FindAsync(id);
            if (noticia != null)
            {
                _context.Noticias.Remove(noticia);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
