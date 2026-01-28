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

        public async Task<IActionResult> Create()
        {
            var vm = new NoticiaViewModel
            {
                TagsDisponiveis = await _context.Tags.OrderBy(t => t.Nome).ToListAsync()
            };
            return PartialView("_NoticiaFormPartial", vm);
        }

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

                _context.NoticiaTags.RemoveRange(noticia.NoticiaTags);
                noticia.NoticiaTags.Clear();
            }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var noticia = await _context.Noticias.FindAsync(id);
            if (noticia == null)
                return Json(new { success = false, message = "Notícia não encontrada." });

            _context.Noticias.Remove(noticia);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

    }
}
