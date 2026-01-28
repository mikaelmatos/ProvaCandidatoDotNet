using System.ComponentModel.DataAnnotations;

namespace ProvaCandidatoDotNet.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required, StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Senha { get; set; } = string.Empty;

        // Relacionamento: um usuário pode criar várias notícias
        public ICollection<Noticia>? Noticias { get; set; }
    }
}
