using Microsoft.EntityFrameworkCore;
using ProvaCandidatoDotNet.Models;

namespace ProvaCandidatoDotNet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NoticiaTag> NoticiaTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NoticiaTag>()
                .HasKey(nt => new { nt.NoticiaId, nt.TagId });

            modelBuilder.Entity<NoticiaTag>()
                .HasOne(nt => nt.Noticia)
                .WithMany(n => n.NoticiaTags)
                .HasForeignKey(nt => nt.NoticiaId);

            modelBuilder.Entity<NoticiaTag>()
                .HasOne(nt => nt.Tag)
                .WithMany(t => t.NoticiaTags)
                .HasForeignKey(nt => nt.TagId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
