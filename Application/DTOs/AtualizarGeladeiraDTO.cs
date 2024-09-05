using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class AtualizarGeladeiraDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Nome { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "A posição deve ser um valor entre 1 e 4.")]
        public int Posicao { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "O andar deve ser um valor entre 1 e 3.")]
        public int Andar { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "O container deve ser um valor entre 1 e 3.")]
        public int Container { get; set; }
    }
}
