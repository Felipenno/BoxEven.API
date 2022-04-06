using BE.Domain.Dtos;
using BE.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BE.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentacaoController : ControllerBase
{
    private readonly IMovimentacaoService _movimentacaoService;

    public MovimentacaoController(IMovimentacaoService movimentacaoService)
    {
        _movimentacaoService = movimentacaoService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarMovimentacaoes()
    {
        try
        {
            var movimetacoes = await _movimentacaoService.ListarMovimentacaoesAsync();

            return Ok(movimetacoes);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar produtos" + ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarProduto([FromBody] MovimentacaoCriarDto movimentacaoDto)
    {
        try
        {
            var concluido = await _movimentacaoService.CriarMovimentacaoAsync(movimentacaoDto);
            if (!concluido)
            {
                return BadRequest("Erro ao realizar uma movimentação");
            }

            return Created("AdicionarProduto", movimentacaoDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar produto" + ex);
        }
    }
}