using BE.Domain.Dtos;
using BE.Domain.Entities;

namespace BE.Domain.Interfaces.Service;

public interface ILocalizacaoService
{
    Task<ResultadoOperacao> CriarLocalizacaoAsync(LocalizacaoCriarDto localizacaoDto);
    Task<bool> VerificarEnderecoDisponivelAsync(int localizacaoId);
    Task<ResultadoOperacao> EditarLocalizacaoAsync(int localizacaoId, LocalizacaoEditarDto localizacaoDto);
    Task<ResultadoOperacao> RemoverLocalizacaoAsync(int localizacaoId);
    Task<LocalizacaoDto> ListarLocalizacaoPorIdAsync(int localizacaoId);
    Task<List<LocalizacaoEnderecoMontadoDto>> ListarLocalizacoesDisponiveisAsync();
    Task<List<LocalizacaoDto>> ListarLocalizacaoAsync(int? produtoId, string? andar, int? corredor, char? lado, int? prateleira);
}