using System.ComponentModel.DataAnnotations;

namespace GestaoConsultasUVV.Models
{
    public class Consulta
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Especialidade { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [StringLength(500)]
        public string? Descricao { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }
    }
}