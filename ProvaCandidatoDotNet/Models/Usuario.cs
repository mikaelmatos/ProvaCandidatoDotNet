using System.ComponentModel.DataAnnotations;

namespace ProvaCandidatoDotNet.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        [StringLength(100)]
        public string Senha { get; set; } = string.Empty;

        public ICollection<Noticia>? Noticias { get; set; }
    }
}
