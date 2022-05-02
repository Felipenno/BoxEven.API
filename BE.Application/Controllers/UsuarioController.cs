using BE.Domain.Dtos;
using BE.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BE.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> RegistrarUsuario(UsuarioRegistroDto usuario)
    {
        try
        {
            var nomeUsuario = await _usuarioService.CadastrarUsuarioAsync(usuario);
            if (nomeUsuario == null)
            {
                return BadRequest();
            }

            return Created("RegistrarUsuario", nomeUsuario);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao registrar" + ex);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UsuarioLoginDto login)
    {
        try
        {
            var usuario = await _usuarioService.LoginAsync(login.Apelido, login.Senha);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao fazer login" + ex);
        }
    }
}
