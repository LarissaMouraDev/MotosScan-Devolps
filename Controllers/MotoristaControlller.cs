using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotosScan.Data;
using MotosScan.Models;

namespace MotosScan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MotoristasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Motoristas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Motorista>>> GetMotoristas(
            [FromQuery] string? status = null,
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanhoPagina = 10)
        {
            var query = _context.Motoristas
                .Include(m => m.MotoAtual)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(m => m.Status == status);
            }

            var motoristas = await query
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return Ok(motoristas);
        }

        // GET: api/Motoristas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Motorista>> GetMotorista(int id)
        {
            var motorista = await _context.Motoristas
                .Include(m => m.MotoAtual)
                .Include(m => m.Manutencoes)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motorista == null)
            {
                return NotFound(new { mensagem = $"Motorista com ID {id} não encontrado" });
            }

            return Ok(motorista);
        }

        // GET: api/Motoristas/cpf/12345678900
        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<Motorista>> GetMotoristaByCPF(string cpf)
        {
            var motorista = await _context.Motoristas
                .Include(m => m.MotoAtual)
                .FirstOrDefaultAsync(m => m.CPF == cpf);

            if (motorista == null)
            {
                return NotFound(new { mensagem = $"Motorista com CPF {cpf} não encontrado" });
            }

            return Ok(motorista);
        }

        // GET: api/Motoristas/cnh/12345678901
        [HttpGet("cnh/{cnh}")]
        public async Task<ActionResult<Motorista>> GetMotoristaByCNH(string cnh)
        {
            var motorista = await _context.Motoristas
                .Include(m => m.MotoAtual)
                .FirstOrDefaultAsync(m => m.CNH == cnh);

            if (motorista == null)
            {
                return NotFound(new { mensagem = $"Motorista com CNH {cnh} não encontrado" });
            }

            return Ok(motorista);
        }

        // POST: api/Motoristas
        [HttpPost]
        public async Task<ActionResult<Motorista>> PostMotorista(Motorista motorista)
        {
            // Validar CPF único
            if (await _context.Motoristas.AnyAsync(m => m.CPF == motorista.CPF))
            {
                return BadRequest(new { mensagem = "CPF já cadastrado" });
            }

            // Validar CNH única
            if (await _context.Motoristas.AnyAsync(m => m.CNH == motorista.CNH))
            {
                return BadRequest(new { mensagem = "CNH já cadastrada" });
            }

            motorista.DataCadastro = DateTime.Now;
            _context.Motoristas.Add(motorista);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMotorista), new { id = motorista.Id }, motorista);
        }

        // PUT: api/Motoristas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotorista(int id, Motorista motorista)
        {
            if (id != motorista.Id)
            {
                return BadRequest(new { mensagem = "ID da URL não corresponde ao ID do motorista" });
            }

            // Validar CPF único (exceto o próprio motorista)
            if (await _context.Motoristas.AnyAsync(m => m.CPF == motorista.CPF && m.Id != id))
            {
                return BadRequest(new { mensagem = "CPF já cadastrado para outro motorista" });
            }

            // Validar CNH única (exceto o próprio motorista)
            if (await _context.Motoristas.AnyAsync(m => m.CNH == motorista.CNH && m.Id != id))
            {
                return BadRequest(new { mensagem = "CNH já cadastrada para outro motorista" });
            }

            _context.Entry(motorista).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotoristaExists(id))
                {
                    return NotFound(new { mensagem = $"Motorista com ID {id} não encontrado" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Motoristas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotorista(int id)
        {
            var motorista = await _context.Motoristas.FindAsync(id);
            if (motorista == null)
            {
                return NotFound(new { mensagem = $"Motorista com ID {id} não encontrado" });
            }

            _context.Motoristas.Remove(motorista);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Motoristas/5/atribuir-moto/3
        [HttpPost("{motoristaId}/atribuir-moto/{motoId}")]
        public async Task<IActionResult> AtribuirMoto(int motoristaId, int motoId)
        {
            var motorista = await _context.Motoristas.FindAsync(motoristaId);
            if (motorista == null)
            {
                return NotFound(new { mensagem = $"Motorista com ID {motoristaId} não encontrado" });
            }

            var moto = await _context.Motos.FindAsync(motoId);
            if (moto == null)
            {
                return NotFound(new { mensagem = $"Moto com ID {motoId} não encontrada" });
            }

            motorista.MotoAtualId = motoId;
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Moto atribuída com sucesso", motorista });
        }

        // DELETE: api/Motoristas/5/remover-moto
        [HttpDelete("{motoristaId}/remover-moto")]
        public async Task<IActionResult> RemoverMoto(int motoristaId)
        {
            var motorista = await _context.Motoristas.FindAsync(motoristaId);
            if (motorista == null)
            {
                return NotFound(new { mensagem = $"Motorista com ID {motoristaId} não encontrado" });
            }

            motorista.MotoAtualId = null;
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Moto removida com sucesso" });
        }

        private bool MotoristaExists(int id)
        {
            return _context.Motoristas.Any(e => e.Id == id);
        }
    }
}