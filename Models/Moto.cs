using System.ComponentModel.DataAnnotations;

namespace MotosScan.Models
{
    public class Moto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Modelo é obrigatório")]
        [StringLength(100, ErrorMessage = "Modelo não pode exceder 100 caracteres")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Placa é obrigatória")]
        [StringLength(10, MinimumLength = 7, ErrorMessage = "Placa inválida")]
        public string Placa { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = "Bom"; // Bom, Regular, Excelente

        [StringLength(100)]
        public string Localizacao { get; set; } = "Pátio A";

        public DateTime? UltimoCheckIn { get; set; }

        public DateTime? UltimoCheckOut { get; set; }

        [StringLength(500)]
        public string? ImagemUrl { get; set; }

        // Relacionamentos
        public ICollection<Manutencao> Manutencoes { get; set; } = new List<Manutencao>();

        public ICollection<Motorista> Motoristas { get; set; } = new List<Motorista>();
    }
}