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

public class MovimentacaoService : IMovimentacaoService
{
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public MovimentacaoService(IMovimentacaoRepository movimentacaoRepository, IUsuarioRepository usuarioRepository, IProdutoRepository produtoRepository, IMapper mapper)
    {
        _movimentacaoRepository = movimentacaoRepository;
        _usuarioRepository = usuarioRepository;
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    public async Task<List<MovimentacaoDto>> ListarMovimentacaoesAsync()
    {
        List<MovimentacaoDto> movimentacaoDtoList = new List<MovimentacaoDto>();

        var movimentacoes = await _movimentacaoRepository.ListarTodosAsync();
        if (movimentacoes != null)
        {
            foreach (var mo in movimentacoes)
            {
                MovimentacaoDto movimentacaoDto = new MovimentacaoDto()
                {
                    MovimentacaoId = mo.MovimentacaoId,
                    Quantidade = mo.Quantidade,
                    Justificativa = mo.Justificativa,
                    DataOperacao = mo.DataOperacao,
                    Tipo = mo.Tipo,
                    Produto = $"{mo.Produto.ProdutoId} - {mo.Produto.Nome}",
                    Usuario = $"{mo.Usuario.Nome} ({mo.Usuario.Apelido})"
                };

                movimentacaoDtoList.Add(movimentacaoDto);
            }
        }

        return movimentacaoDtoList;
    }

    public Task<MovimentacaoDto> ListarPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CriarMovimentacaoAsync(MovimentacaoCriarDto moDto)
    {
        var produto = await _produtoRepository.ListarPorIdAsync(moDto.ProdutoId);
        if(produto == null) { return false; }

        var usuario = await _usuarioRepository.ListarPorIdAsync(moDto.UsuarioId);
        if (usuario == null) { return false; }

        int quantidadeAlterada = produto.Quantidade.Value;
        if(moDto.Tipo == "ENTRADA")
        {
            quantidadeAlterada += moDto.Quantidade;
        }
        else if(moDto.Tipo == "SAIDA")
        {
            if(quantidadeAlterada < moDto.Quantidade)
            {
                return false;
            }

            quantidadeAlterada -= moDto.Quantidade;
        }
        else
        {
            return false;
        }

        var produtoAlterado = await _produtoRepository.AlterarQuantidade(quantidadeAlterada, produto.ProdutoId, DateTime.Now);
        if(produtoAlterado)
        {
            var movimentacao = new Movimentacao(moDto.Quantidade, moDto.Justificativa, DateTime.Now, moDto.Tipo, produto, usuario);

            var conluido = await _movimentacaoRepository.AdicionarAsync(movimentacao);

            return conluido;
        }

        return false;
        
    }
}