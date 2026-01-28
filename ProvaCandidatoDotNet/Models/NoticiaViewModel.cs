using System.ComponentModel.DataAnnotations;

namespace ProvaCandidatoDotNet.Models
{
    public class NoticiaViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string Conteudo { get; set; } = string.Empty;

        public DateTime DataPublicacao { get; set; } = DateTime.UtcNow;

        public List<Tag> TagsDisponiveis { get; set; } = new();
        public List<int> SelectedTagIds { get; set; } = new();
    }
}
