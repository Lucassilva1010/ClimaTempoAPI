using ClimaApi.Dados;
using ClimaApi.Servicos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClimaController : ControllerBase
{
    private readonly ClimaServico _servico;
    private readonly ClimaDbContext _db;

    public ClimaController(ClimaServico servico, ClimaDbContext db)
    {
        _servico = servico;
        _db = db;
    }

    // ✅ Apenas consulta clima atual (não salva)
    [HttpGet("atual")]
    public async Task<IActionResult> ClimaAtual([FromQuery] string cidade)
    {
        if (string.IsNullOrWhiteSpace(cidade))
            return BadRequest("Parametro 'cidade' é obrigatório.");

        var registro = await _servico.ObterClimaAtualAsync(cidade);
        return Ok(registro);
    }

    // ✅ POST para salvar no banco
    [HttpPost("salvar")]
    public async Task<IActionResult> Salvar([FromQuery] string cidade)
    {
        if (string.IsNullOrWhiteSpace(cidade))
            return BadRequest("Parametro 'cidade' é obrigatório.");

        var registro = await _servico.ObterClimaAtualAsync(cidade);

        _db.RegistrosClima.Add(registro);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Historico), new { id = registro.Id }, registro);
    }

    // ✅ GET histórico (consulta dados salvos no banco)
    [HttpGet("historico")]
    public IActionResult Historico()
    {
        return Ok(_db.RegistrosClima.OrderByDescending(r => r.CriadoEmUtc).ToList());
    }
}
