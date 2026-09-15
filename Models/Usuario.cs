using System.ComponentModel.DataAnnotations;

namespace GestaoConsultasUVV.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Senha { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}