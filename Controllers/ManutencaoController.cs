using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotosScan.Data;
using MotosScan.Models;

namespace MotosScan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManutencoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ManutencoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Manutencoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manutencao>>> GetManutencoes(
            [FromQuery] string? status = null,
            [FromQuery] string? tipo = null,
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanhoPagina = 10)
        {
            var query = _context.Manutencoes
                .Include(m => m.Moto)
                .Include(m => m.Motorista)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(m => m.Status == status);
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                query = query.Where(m => m.TipoManutencao == tipo);
            }

            var manutencoes = await query
                .OrderByDescending(m => m.DataManutencao)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return Ok(manutencoes);
        }

        // GET: api/Manutencoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manutencao>> GetManutencao(int id)
        {
            var manutencao = await _context.Manutencoes
                .Include(m => m.Moto)
                .Include(m => m.Motorista)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (manutencao == null)
            {
                return NotFound(new { mensagem = $"Manutenção com ID {id} não encontrada" });
            }

            return Ok(manutencao);
        }

        // GET: api/Manutencoes/moto/5
        [HttpGet("moto/{motoId}")]
        public async Task<ActionResult<IEnumerable<Manutencao>>> GetManutencoesByMoto(int motoId)
        {
            var manutencoes = await _context.Manutencoes
                .Include(m => m.Motorista)
                .Where(m => m.MotoId == motoId)
                .OrderByDescending(m => m.DataManutencao)
                .ToListAsync();

            if (!manutencoes.Any())
            {
                return NotFound(new { mensagem = $"Nenhuma manutenção encontrada para a moto ID {motoId}" });
            }

            return Ok(manutencoes);
        }

        // GET: api/Manutencoes/motorista/5
        [HttpGet("motorista/{motoristaId}")]
        public async Task<ActionResult<IEnumerable<Manutencao>>> GetManutencoesByMotorista(int motoristaId)
        {
            var manutencoes = await _context.Manutencoes
                .Include(m => m.Moto)
                .Where(m => m.MotoristaId == motoristaId)
                .OrderByDescending(m => m.DataManutencao)
                .ToListAsync();

            if (!manutencoes.Any())
            {
                return NotFound(new { mensagem = $"Nenhuma manutenção encontrada para o motorista ID {motoristaId}" });
            }

            return Ok(manutencoes);
        }

        // GET: api/Manutencoes/pendentes
        [HttpGet("pendentes")]
        public async Task<ActionResult<IEnumerable<Manutencao>>> GetManutencoesPendentes()
        {
            var manutencoes = await _context.Manutencoes
                .Include(m => m.Moto)
                .Include(m => m.Motorista)
                .Where(m => m.Status == "Pendente" || m.Status == "Em Andamento")
                .OrderBy(m => m.DataManutencao)
                .ToListAsync();

            return Ok(manutencoes);
        }

        // POST: api/Manutencoes
        [HttpPost]
        public async Task<ActionResult<Manutencao>> PostManutencao(Manutencao manutencao)
        {
            // Validar se a moto existe
            var motoExists = await _context.Motos.AnyAsync(m => m.Id == manutencao.MotoId);
            if (!motoExists)
            {
                return BadRequest(new { mensagem = $"Moto com ID {manutencao.MotoId} não encontrada" });
            }

            // Validar se o motorista existe (se informado)
            if (manutencao.MotoristaId.HasValue)
            {
                var motoristaExists = await _context.Motoristas.AnyAsync(m => m.Id == manutencao.MotoristaId);
                if (!motoristaExists)
                {
                    return BadRequest(new { mensagem = $"Motorista com ID {manutencao.MotoristaId} não encontrado" });
                }
            }

            manutencao.DataCriacao = DateTime.Now;
            _context.Manutencoes.Add(manutencao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetManutencao), new { id = manutencao.Id }, manutencao);
        }

        // PUT: api/Manutencoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManutencao(int id, Manutencao manutencao)
        {
            if (id != manutencao.Id)
            {
                return BadRequest(new { mensagem = "ID da URL não corresponde ao ID da manutenção" });
            }

            // Validar se a moto existe
            var motoExists = await _context.Motos.AnyAsync(m => m.Id == manutencao.MotoId);
            if (!motoExists)
            {
                return BadRequest(new { mensagem = $"Moto com ID {manutencao.MotoId} não encontrada" });
            }

            _context.Entry(manutencao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManutencaoExists(id))
                {
                    return NotFound(new { mensagem = $"Manutenção com ID {id} não encontrada" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Manutencoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManutencao(int id)
        {
            var manutencao = await _context.Manutencoes.FindAsync(id);
            if (manutencao == null)
            {
                return NotFound(new { mensagem = $"Manutenção com ID {id} não encontrada" });
            }

            _context.Manutencoes.Remove(manutencao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Manutencoes/5/concluir
        [HttpPost("{id}/concluir")]
        public async Task<IActionResult> ConcluirManutencao(int id, [FromBody] ConcluirManutencaoDto dto)
        {
            var manutencao = await _context.Manutencoes.FindAsync(id);
            if (manutencao == null)
            {
                return NotFound(new { mensagem = $"Manutenção com ID {id} não encontrada" });
            }

            manutencao.Status = "Concluída";
            manutencao.DataConclusao = DateTime.Now;

            if (dto.Custo.HasValue)
                manutencao.Custo = dto.Custo;

            if (!string.IsNullOrEmpty(dto.Observacoes))
                manutencao.Observacoes = dto.Observacoes;

            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Manutenção concluída com sucesso", manutencao });
        }

        // POST: api/Manutencoes/5/cancelar
        [HttpPost("{id}/cancelar")]
        public async Task<IActionResult> CancelarManutencao(int id, [FromBody] CancelarManutencaoDto dto)
        {
            var manutencao = await _context.Manutencoes.FindAsync(id);
            if (manutencao == null)
            {
                return NotFound(new { mensagem = $"Manutenção com ID {id} não encontrada" });
            }

            manutencao.Status = "Cancelada";

            if (!string.IsNullOrEmpty(dto.Motivo))
                manutencao.Observacoes = $"CANCELADA: {dto.Motivo}";

            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Manutenção cancelada com sucesso", manutencao });
        }

        private bool ManutencaoExists(int id)
        {
            return _context.Manutencoes.Any(e => e.Id == id);
        }
    }

    // DTOs para operações específicas
    public class ConcluirManutencaoDto
    {
        public decimal? Custo { get; set; }
        public string? Observacoes { get; set; }
    }

    public class CancelarManutencaoDto
    {
        public string? Motivo { get; set; }
    }
}