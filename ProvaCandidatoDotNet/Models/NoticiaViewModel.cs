using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProvaCandidatoDotNet.Models
{
    public class NoticiaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Título é obrigatório.")]
        [StringLength(200, ErrorMessage = "O Título deve ter no máximo 200 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Conteúdo é obrigatório.")]
        public string Conteudo { get; set; } = string.Empty;

        public DateTime DataPublicacao { get; set; } = DateTime.UtcNow;

        public int? UsuarioId { get; set; }

        public List<Usuario> UsuariosDisponiveis { get; set; } = new();
        public List<Tag> TagsDisponiveis { get; set; } = new();
        public List<int> SelectedTagIds { get; set; } = new();
    }
}
