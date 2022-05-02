using BE.Domain.Dtos;
using BE.Domain.Enum;
using BE.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BE.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _PedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _PedidoService = pedidoService;
    }

    [HttpGet]
    public async Task<IActionResult> ListasPedidos()
    {
        try
        {
            var pedidos = await _PedidoService.ListasPedidosSeparacao();

            return Ok(pedidos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar pedidos" + ex);
        }
    }

    [HttpGet("filtro")]
    public async Task<IActionResult> ListasPedidosFiltro([FromQuery] StatusPedido status, [FromQuery] DateTime conclusao)
    {
        try
        {
            var pedidos = await _PedidoService.ListarPedidosPorFiltroAsync(status, conclusao);

            return Ok(pedidos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar pedidos" + ex);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> AlterarStatusPedido([FromBody] PedidoAlterarStatusDto pedido)
    {
        try
        {
            await _PedidoService.AtualizarStatus(pedido);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar pedidos" + ex);
        }
    }
}
