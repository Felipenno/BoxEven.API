using AutoMapper;
using BE.Domain.Dtos;
using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using BE.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Service;

public class LocalizacaoService : ILocalizacaoService
{
    private readonly ILocalizacaoRepository _localizacaoRepository;
    private readonly IMapper _mapper;

    public LocalizacaoService(ILocalizacaoRepository localizacaRepository, IMapper mapper)
    {
        _localizacaoRepository = localizacaRepository;
        _mapper = mapper;
    }

    public async Task<ResultadoOperacao> CriarLocalizacaoAsync(LocalizacaoCriarDto localizacaoDto)
    {
        var localizacao = _mapper.Map<Localizacao>(localizacaoDto);

        var localizacaoExiste = await _localizacaoRepository.VerificarExistenciaAsync(localizacao);
        if (localizacaoExiste)
        {
            return new ResultadoOperacao(false, "Localização já existe");
        }

        if(localizacao.ProdutoId != null && localizacao.ProdutoId > 0)
        {
            var produtoLimite = await _localizacaoRepository.ProdutoLimiteAtingidoAsync(localizacao.ProdutoId);
            if (produtoLimite)
            {
                return new ResultadoOperacao(false, "Um produto não pode ter mais que 3 locais");
            }
        }

        var concluido = await _localizacaoRepository.AdicionarAsync(localizacao);
        return new ResultadoOperacao(concluido, "Não foi possível criar essa localização");
    }

    public async Task<ResultadoOperacao> EditarLocalizacaoAsync(int localizacaoId, LocalizacaoEditarDto localizacaoDto)
    {
        var localizacaoExistente = await _localizacaoRepository.ListarPorIdAsync(localizacaoId);
        if(localizacaoExistente == null || localizacaoExistente.LocalizacaoId != localizacaoDto.LocalizacaoId)
        {
            return new ResultadoOperacao(false, "Localização não existe.");
        }

        var localizacao = _mapper.Map<Localizacao>(localizacaoDto);

        if (!localizacaoExistente.EnderecoIgual(localizacao))
        {
            var localizacaoExiste = await _localizacaoRepository.VerificarExistenciaAsync(localizacao);
            if (localizacaoExiste)
            {
                return new ResultadoOperacao(false, "Localização já existe");
            }
        }

        if (localizacao.ProdutoId != null && localizacao.ProdutoId > 0)
        {
            var produtoLimite = await _localizacaoRepository.ProdutoLimiteAtingidoAsync(localizacao.ProdutoId);
            if (produtoLimite)
            {
                return new ResultadoOperacao(false, "Um produto não pode ter mais que 3 locais");
            }
        }

        var concluido =  await _localizacaoRepository.AtualizarAsync(localizacao);
        return new ResultadoOperacao(concluido, "Não foi possível editar essa localização");
    }

    public async Task<List<LocalizacaoDto>> ListarLocalizacaoAsync(int? produtoId, string? andar, int? corredor, char? lado, int? prateleira)
    {
        var localizacao = await _localizacaoRepository.ListarTodosAsync(produtoId, andar, corredor, lado, prateleira);
        return  _mapper.Map<List<LocalizacaoDto>>(localizacao);
    }

    public async Task<LocalizacaoDto> ListarLocalizacaoPorIdAsync(int localizacaoId)
    {
        var localizacao = await _localizacaoRepository.ListarPorIdAsync(localizacaoId);
        return _mapper.Map<LocalizacaoDto>(localizacao);
    }

    public async Task<List<LocalizacaoEnderecoMontadoDto>> ListarLocalizacoesDisponiveisAsync()
    {
        List<LocalizacaoEnderecoMontadoDto> localizacoesDto = new List<LocalizacaoEnderecoMontadoDto>();

        var localizacoes = await _localizacaoRepository.ListarTodosDisponiveisAsync();
        foreach(var item in localizacoes)
        {
            localizacoesDto.Add(new LocalizacaoEnderecoMontadoDto { LocalizacaoId = item.LocalizacaoId, Endereco = item.MontarEndereco() });
        }

        return localizacoesDto;
    }

    public async Task<ResultadoOperacao> RemoverLocalizacaoAsync(int localizacaoId)
    {
        var concluido = await _localizacaoRepository.RemoveAsync(localizacaoId);
        return new ResultadoOperacao(concluido, "Não foi possível excluir essa localização.");
    }

    public async Task<bool> VerificarEnderecoDisponivelAsync(int localizacaoId)
    {
        return await _localizacaoRepository.VerificarDisponibilidadeAsync(localizacaoId);
    }

}