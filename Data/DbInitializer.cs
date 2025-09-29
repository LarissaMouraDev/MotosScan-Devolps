using MotosScan.Models;

namespace MotosScan.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Verifica se já existem dados
            if (context.Motos.Any())
            {
                return; // Banco de dados já foi populado
            }

            // Seed de Motos
            var motos = new Moto[]
            {
                new Moto
                {
                    Modelo = "Honda CG 160",
                    Placa = "ABC1234",
                    Estado = "Excelente",
                    Localizacao = "Pátio A",
                    UltimoCheckIn = DateTime.Now.AddDays(-2)
                },
                new Moto
                {
                    Modelo = "Yamaha Factor 150",
                    Placa = "DEF5678",
                    Estado = "Bom",
                    Localizacao = "Pátio B",
                    UltimoCheckIn = DateTime.Now.AddDays(-1)
                },
                new Moto
                {
                    Modelo = "Honda Biz 125",
                    Placa = "GHI9012",
                    Estado = "Regular",
                    Localizacao = "Oficina",
                    UltimoCheckIn = DateTime.Now.AddDays(-5)
                },
                new Moto
                {
                    Modelo = "Suzuki Intruder 150",
                    Placa = "JKL3456",
                    Estado = "Excelente",
                    Localizacao = "Pátio A",
                    UltimoCheckIn = DateTime.Now.AddHours(-3)
                },
                new Moto
                {
                    Modelo = "Yamaha XTZ 150",
                    Placa = "MNO7890",
                    Estado = "Bom",
                    Localizacao = "Em Rota",
                    UltimoCheckOut = DateTime.Now.AddHours(-2)
                }
            };

            context.Motos.AddRange(motos);
            context.SaveChanges();

            // Seed de Motoristas
            var motoristas = new Motorista[]
            {
                new Motorista
                {
                    Nome = "João Silva",
                    CPF = "12345678900",
                    CNH = "12345678901",
                    CategoriaCNH = "AB",
                    Telefone = "11987654321",
                    Email = "joao.silva@mottu.com",
                    Status = "Ativo",
                    DataAdmissao = DateTime.Now.AddMonths(-12),
                    MotoAtualId = motos[0].Id
                },
                new Motorista
                {
                    Nome = "Maria Santos",
                    CPF = "98765432100",
                    CNH = "98765432101",
                    CategoriaCNH = "A",
                    Telefone = "11876543210",
                    Email = "maria.santos@mottu.com",
                    Status = "Ativo",
                    DataAdmissao = DateTime.Now.AddMonths(-8),
                    MotoAtualId = motos[4].Id
                },
                new Motorista
                {
                    Nome = "Carlos Oliveira",
                    CPF = "45678912300",
                    CNH = "45678912301",
                    CategoriaCNH = "AB",
                    Telefone = "11765432109",
                    Email = "carlos.oliveira@mottu.com",
                    Status = "Ativo",
                    DataAdmissao = DateTime.Now.AddMonths(-6),
                    MotoAtualId = motos[1].Id
                },
                new Motorista
                {
                    Nome = "Ana Pereira",
                    CPF = "78912345600",
                    CNH = "78912345601",
                    CategoriaCNH = "A",
                    Telefone = "11654321098",
                    Email = "ana.pereira@mottu.com",
                    Status = "Férias",
                    DataAdmissao = DateTime.Now.AddMonths(-18)
                },
                new Motorista
                {
                    Nome = "Pedro Costa",
                    CPF = "32165498700",
                    CNH = "32165498701",
                    CategoriaCNH = "AB",
                    Telefone = "11543210987",
                    Email = "pedro.costa@mottu.com",
                    Status = "Inativo",
                    DataAdmissao = DateTime.Now.AddMonths(-24)
                }
            };

            context.Motoristas.AddRange(motoristas);
            context.SaveChanges();

            // Seed de Manutenções
            var manutencoes = new Manutencao[]
            {
                new Manutencao
                {
                    MotoId = motos[0].Id,
                    MotoristaId = motoristas[0].Id,
                    TipoManutencao = "Preventiva",
                    Descricao = "Troca de óleo e filtro",
                    DataManutencao = DateTime.Now.AddDays(-30),
                    DataConclusao = DateTime.Now.AddDays(-30),
                    Status = "Concluída",
                    Custo = 150.00M,
                    Quilometragem = 15000,
                    Oficina = "Oficina Central",
                    Observacoes = "Troca realizada conforme programação"
                },
                new Manutencao
                {
                    MotoId = motos[1].Id,
                    MotoristaId = motoristas[2].Id,
                    TipoManutencao = "Revisão",
                    Descricao = "Revisão dos 10.000 km",
                    DataManutencao = DateTime.Now.AddDays(-15),
                    DataConclusao = DateTime.Now.AddDays(-14),
                    Status = "Concluída",
                    Custo = 350.00M,
                    Quilometragem = 10000,
                    Oficina = "Oficina Yamaha",
                    Observacoes = "Revisão completa realizada"
                },
                new Manutencao
                {
                    MotoId = motos[2].Id,
                    TipoManutencao = "Corretiva",
                    Descricao = "Substituição da corrente",
                    DataManutencao = DateTime.Now.AddDays(-7),
                    Status = "Em Andamento",
                    Quilometragem = 18500,
                    Oficina = "Oficina Central",
                    Observacoes = "Aguardando peça"
                },
                new Manutencao
                {
                    MotoId = motos[3].Id,
                    MotoristaId = motoristas[0].Id,
                    TipoManutencao = "Preventiva",
                    Descricao = "Limpeza do filtro de ar",
                    DataManutencao = DateTime.Now.AddDays(-3),
                    DataConclusao = DateTime.Now.AddDays(-3),
                    Status = "Concluída",
                    Custo = 80.00M,
                    Quilometragem = 8000,
                    Oficina = "Oficina Express"
                },
                new Manutencao
                {
                    MotoId = motos[4].Id,
                    MotoristaId = motoristas[1].Id,
                    TipoManutencao = "Corretiva",
                    Descricao = "Troca de pneu traseiro",
                    DataManutencao = DateTime.Now.AddDays(-20),
                    DataConclusao = DateTime.Now.AddDays(-19),
                    Status = "Concluída",
                    Custo = 280.00M,
                    Quilometragem = 22000,
                    Oficina = "Pneus e Cia",
                    Observacoes = "Pneu com desgaste excessivo"
                },
                new Manutencao
                {
                    MotoId = motos[0].Id,
                    TipoManutencao = "Preventiva",
                    Descricao = "Troca de pastilhas de freio",
                    DataManutencao = DateTime.Now.AddDays(5),
                    Status = "Pendente",
                    Quilometragem = 16000,
                    Oficina = "Oficina Central"
                },
                new Manutencao
                {
                    MotoId = motos[1].Id,
                    TipoManutencao = "Revisão",
                    Descricao = "Revisão dos 20.000 km",
                    DataManutencao = DateTime.Now.AddDays(10),
                    Status = "Pendente",
                    Quilometragem = 19500,
                    Oficina = "Oficina Yamaha"
                }
            };

            context.Manutencoes.AddRange(manutencoes);
            context.SaveChanges();
        }
    }
}