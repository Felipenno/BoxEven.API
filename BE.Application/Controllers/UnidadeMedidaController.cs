using BE.Domain.Dtos;
using BE.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BE.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnidadeMedidaController : ControllerBase
{
    private readonly IUnidadeMedidaService _unidadeMedidaService;

    public UnidadeMedidaController(IUnidadeMedidaService unidadeMedidaService)
    {
        _unidadeMedidaService = unidadeMedidaService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarUnidadeMedida()
    {
        try
        {
            var unidadeMedida = await _unidadeMedidaService.ListarUnidadeMedidaAsync();
            if(unidadeMedida == null)
            {
                return NoContent();
            }

            return Ok(unidadeMedida);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro unidadeMedida: " + ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CriarUnidadeMedida(UnidadeMedidaDto unidadeMedidaDto)
    {
        try
        {
            var sucesso = await _unidadeMedidaService.CriarUnidadeMedidarAsync(unidadeMedidaDto);
            if (!sucesso)
            {
                return BadRequest();
            }

            return Created("AdicionarUnidadeMedida", unidadeMedidaDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro unidadeMedida: " + ex);
        }
    }

    [HttpPut]
    public async Task<IActionResult> EditarUnidadeMedida(UnidadeMedidaDto unidadeMedidaDto)
    {
        try
        {
            var sucesso = await _unidadeMedidaService.EditarUnidadeMedidaAsync(unidadeMedidaDto);
            if (!sucesso)
            {
                return BadRequest();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro unidadeMedida: " + ex);
        }
    }

    [HttpDelete("{unidadeMedidaId}")]
    public async Task<IActionResult> RemoverUnidadeMedida(int unidadeMedidaId)
    {
        try
        {
            var sucesso = await _unidadeMedidaService.RemoveUnidadeMedidaAsync(unidadeMedidaId);

            return Ok(sucesso);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro unidadeMedida: " + ex);
        }
    }
}