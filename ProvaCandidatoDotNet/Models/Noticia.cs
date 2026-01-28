using System.ComponentModel.DataAnnotations;

namespace ProvaCandidatoDotNet.Models
{
    public class Noticia
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string Conteudo { get; set; } = string.Empty;

        public DateTime DataPublicacao { get; set; } = DateTime.UtcNow;

        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public ICollection<NoticiaTag> NoticiaTags { get; set; } = new List<NoticiaTag>();
    }
}
