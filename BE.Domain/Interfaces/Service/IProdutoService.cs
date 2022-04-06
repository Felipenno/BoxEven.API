using BE.Domain.Dtos;

namespace BE.Domain.Interfaces.Service;

public interface IProdutoService
{
    Task<bool> CriarProdutoAsync(ProdutoCriarDto produtoDto);
    Task<bool> AtualizarProdutoAsync(ProdutoEditarDto produtoDto, int id);
    Task<List<ProdutoListarDto>> ListarProdutosAsync(bool? status, int? id, string? nome);
    Task<List<ProdutoListarDto>> ListarProdutosDesalocadosAsync();
    Task<ProdutoListarDto> ListarPorIdAsync(int produtoId);
}