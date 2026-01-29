using System.ComponentModel.DataAnnotations;

namespace ProvaCandidatoDotNet.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        public ICollection<NoticiaTag> NoticiaTags { get; set; } = new List<NoticiaTag>();
    }
}
