using BE.Domain.Dtos;
using BE.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BE.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocalizacaoController : ControllerBase
{
    private readonly ILocalizacaoService _localizacaoService;

    public LocalizacaoController(ILocalizacaoService localizacaoService)
    {
        _localizacaoService = localizacaoService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarLocalizacoes([FromQuery(Name = "ProdutoId")] int? produtoId, [FromQuery(Name = "Andar")] string? andar, [FromQuery(Name = "Corredor")] int? corredor, [FromQuery(Name = "Lado")] char? lado, [FromQuery(Name = "Prateleira")] int? prateleira)
    {
        try
        {
            var localizacoes = await _localizacaoService.ListarLocalizacaoAsync(produtoId, andar, corredor, lado, prateleira);
            if (localizacoes == null)
            {
                return NoContent();
            }

            return Ok(localizacoes);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro localizacao: " + ex);
        }
    }

    [HttpGet("LocalizacoesDisponiveis")]
    public async Task<IActionResult> ListarLocalizacoesDisponiveis()
    {
        try
        {
            var localizacoes = await _localizacaoService.ListarLocalizacoesDisponiveisAsync();
            if (localizacoes == null)
            {
                return NoContent();
            }

            return Ok(localizacoes);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro localizacao: " + ex);
        }
    }

    [HttpGet("EnderecoDisponivel/{localizacaoId}")]
    public async Task<IActionResult> EnderecoDisponivel([FromRoute] int localizacaoId)
    {
        try
        {
            var disponivel = await _localizacaoService.VerificarEnderecoDisponivelAsync(localizacaoId);

            return Ok(disponivel);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro localizacao: " + ex);
        }
    }

    [HttpGet("{localizacaoId}")]
    public async Task<IActionResult> ListarLocalizacaoPorId([FromRoute] int localizacaoId)
    {
        try
        {
            var localizacao = await _localizacaoService.ListarLocalizacaoPorIdAsync(localizacaoId);
            if (localizacao == null)
            {
                return NotFound();
            }

            return Ok(localizacao);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro localizacao: " + ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarLocalizacao(LocalizacaoCriarDto localizacaoDto)
    {
        try
        {
            var resultado = await _localizacaoService.CriarLocalizacaoAsync(localizacaoDto);
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado.ErroMsg);
            }

            return Created("AdicionarLocalizacao", localizacaoDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro localizacao: " + ex);
        }
    }

    [HttpPut("{localizacaoId}")]
    public async Task<IActionResult> AtualizarLocalizacao([FromBody] LocalizacaoEditarDto localizacaoDto, [FromRoute] int localizacaoId)
    {
        try
        {
            var resultado = await _localizacaoService.EditarLocalizacaoAsync(localizacaoId, localizacaoDto);
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado.ErroMsg);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro localizacao: " + ex);
        }
    }

    [HttpDelete("{localizacaoId}")]
    public async Task<IActionResult> RemoverLocalizacao([FromRoute] int localizacaoId)
    {
        try
        {
            var sucesso = await _localizacaoService.RemoverLocalizacaoAsync(localizacaoId);

            return Ok(sucesso);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro localizacao: " + ex);
        }
    }
}