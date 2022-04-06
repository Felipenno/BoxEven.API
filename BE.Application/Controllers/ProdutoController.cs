using BE.Domain.Dtos;
using BE.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BE.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarProdutos([FromQuery(Name = "status")] bool? status = null, [FromQuery(Name = "id")] int? id = 0, [FromQuery(Name = "descricao")] string? descricao = null)
    {
        try
        {
            if (id < 0 || (!string.IsNullOrWhiteSpace(descricao) && descricao.Length < 3))
            {
                return BadRequest("Dados Inválidos, id deve se válido e descrição deve ter no minímo 3 caracteres");
            }

            var produtos = await _produtoService.ListarProdutosAsync(status, id, descricao);

            return Ok(produtos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar produtos" + ex);
        }
    }

    [HttpGet("desalocado")]
    public async Task<IActionResult> ListarProdutosDesalocados()
    {
        try
        {
            var produtos = await _produtoService.ListarProdutosDesalocadosAsync();

            return Ok(produtos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar produtos" + ex);
        }
    }

    [HttpGet("{produtoId}")]
    public async Task<IActionResult> ListarProdutoPorId([FromRoute] int produtoId)
    {
        try
        {
            if (produtoId < 1)
            {
                return BadRequest();
            }

            var produto = await _produtoService.ListarPorIdAsync(produtoId);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar produtos" + ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarProduto([FromBody] ProdutoCriarDto produtoDto)
    {
        try
        {
            var sucesso = await _produtoService.CriarProdutoAsync(produtoDto);
            if (!sucesso)
            {
                return BadRequest("Não foi possivel criar um produto com os dados passados");
            }

            return Created("AdicionarProduto", produtoDto.Nome);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar produto" + ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarProduto([FromBody] ProdutoEditarDto produtoDto, [FromRoute] int id)
    {
        try
        {
            var sucesso = await _produtoService.AtualizarProdutoAsync(produtoDto, id);
            if (!sucesso)
            {
                return NotFound();
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar produto" + ex);
        }
    }
}

