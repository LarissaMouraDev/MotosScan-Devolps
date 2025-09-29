using System.ComponentModel.DataAnnotations;

namespace MotosScan.Models
{
    public class Manutencao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Moto é obrigatória")]
        public int MotoId { get; set; }
        public Moto Moto { get; set; } = null!;

        public int? MotoristaId { get; set; }
        public Motorista? Motorista { get; set; }

        [Required(ErrorMessage = "Tipo de manutenção é obrigatório")]
        [StringLength(50)]
        public string TipoManutencao { get; set; } = string.Empty; // Preventiva, Corretiva, Revisão

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(500)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public DateTime DataManutencao { get; set; } = DateTime.Now;

        public DateTime? DataConclusao { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Pendente"; // Pendente, Em Andamento, Concluída, Cancelada

        [Range(0, double.MaxValue, ErrorMessage = "Custo deve ser um valor positivo")]
        public decimal? Custo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quilometragem deve ser um valor positivo")]
        public int? Quilometragem { get; set; }

        [StringLength(100)]
        public string? Oficina { get; set; }

        [StringLength(1000)]
        public string? Observacoes { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}