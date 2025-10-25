using System.ComponentModel.DataAnnotations;

namespace MotosScan.Models
{
    public class Motorista
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome não pode exceder 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF inválido")]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "CNH é obrigatória")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CNH deve ter 11 dígitos")]
        public string CNH { get; set; } = string.Empty;

        [StringLength(20)]
        public string CategoriaCNH { get; set; } = "A"; // A, AB, etc

        [StringLength(15)]
        [Phone(ErrorMessage = "Telefone inválido")]
        public string? Telefone { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Ativo"; // Ativo, Inativo, Férias

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public DateTime? DataAdmissao { get; set; }

        // Relacionamento com Moto (um motorista pode estar associado a uma moto)
        public int? MotoAtualId { get; set; }
        public Moto? MotoAtual { get; set; }

        // Navegação para histórico de manutenções
        public ICollection<Manutencao> Manutencoes { get; set; } = new List<Manutencao>();
    }
}