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
    public async Task<IActionResult> GetAll()
    {
        return Ok();
    }
}

