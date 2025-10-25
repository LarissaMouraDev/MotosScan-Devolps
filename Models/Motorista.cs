using System.ComponentModel.DataAnnotations;

namespace MotosScan.Models
{
    public class Motorista
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome � obrigat�rio")]
        [StringLength(100, ErrorMessage = "Nome n�o pode exceder 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "CPF � obrigat�rio")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF inv�lido")]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "CNH � obrigat�ria")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CNH deve ter 11 d�gitos")]
        public string CNH { get; set; } = string.Empty;

        [StringLength(20)]
        public string CategoriaCNH { get; set; } = "A"; // A, AB, etc

        [StringLength(15)]
        [Phone(ErrorMessage = "Telefone inv�lido")]
        public string? Telefone { get; set; }

        [EmailAddress(ErrorMessage = "Email inv�lido")]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Ativo"; // Ativo, Inativo, F�rias

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public DateTime? DataAdmissao { get; set; }

        // Relacionamento com Moto (um motorista pode estar associado a uma moto)
        public int? MotoAtualId { get; set; }
        public Moto? MotoAtual { get; set; }

        // Navega��o para hist�rico de manuten��es
        public ICollection<Manutencao> Manutencoes { get; set; } = new List<Manutencao>();
    }
}