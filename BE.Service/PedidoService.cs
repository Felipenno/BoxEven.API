using BE.Domain.Dtos;
using BE.Domain.Entities;
using BE.Domain.Enum;
using BE.Domain.Interfaces;
using BE.Domain.Interfaces.Repository;
using BE.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Service;

public class PedidoService : IPedidoService
{
    private readonly IVendasInfraServices _vendasContext;
    private readonly IArquivosService _arquivosService;
    private readonly IMovimentacaoService _movimentacoService;

    public PedidoService(IVendasInfraServices vendasContext, IArquivosService arquivosService, IMovimentacaoService movimentacoService)
    {
        _arquivosService = arquivosService;
        _vendasContext = vendasContext;
        _movimentacoService = movimentacoService;
    }

    public async Task AtualizarStatus(PedidoAlterarStatusDto pedido)
    {
        if(pedido.StatusPedido == StatusPedido.Concluido)
        {
            var movimentacaoDto = new MovimentacaoCriarDto()
            {
                UsuarioId = pedido.IdUsuario,
                Justificativa = $"Saída automática: {pedido.NumeroPedido}",
                Tipo = "SAIDA"
            };

            bool concluido = false;

            foreach(var item in pedido.Produtos)
            {
                movimentacaoDto.ProdutoId = item.IdProduto;
                movimentacaoDto.Quantidade = item.Quantidade;
                concluido = await _movimentacoService.CriarMovimentacaoAsync(movimentacaoDto);
            }

            if (concluido)
            {
                await _vendasContext.AlterarStatusPedidoAsync(pedido.IdPedido, pedido.StatusPedido);
            }
        }

        if(pedido.StatusPedido == StatusPedido.Cancelado)
        {
            await _vendasContext.AlterarStatusPedidoAsync(pedido.IdPedido, pedido.StatusPedido);
        } 
    }

    public async Task<List<PedidoDto>> ListarPedidosPorFiltroAsync(StatusPedido status, DateTime conclusao)
    {
        var pedidosDto = new List<PedidoDto>();

        var pedidos = await _vendasContext.ListarPedidosPorFiltroAsync(status, conclusao);
        foreach (var pedido in pedidos)
        {
            var pedidoDto = new PedidoDto
            {
                Id = pedido.Id,
                Numero = pedido.Numero,
                Vendedor = pedido.Vendedor,
                Status = pedido.Status,
                Criacao = pedido.Criacao,
                Conclusao = pedido.Conclusao,
                Produtos = new List<ProdutoListarDto>()
            };

            foreach (var produto in pedido.Produtos)
            {
                var produtoDto = new ProdutoListarDto()
                {
                    ProdutoId = produto.ProdutoId,
                    Ativo = produto.Ativo,
                    Nome = produto.Nome,
                    Quantidade = produto.Quantidade,
                    Preco = produto.Preco,
                    CodigoBarras = produto.CodigoBarras,
                    Imagem = await _arquivosService.RecuperarImagemProduto(produto.ImagemNome, produto.ImagemTipo),
                    Criacao = produto.Criacao,
                    Atualizacao = produto.Atualizacao,
                    UnidadeMedida = new UnidadeMedidaDto() { UnidadeMedidaId = produto.UnidadeMedida.UnidadeMedidaId, Descricao = produto.UnidadeMedida.Descricao },
                    Localizacoes = new List<LocalizacaoEnderecoMontadoDto>()
                };

                foreach (var local in produto.Localizacoes)
                {
                    var localizacaoDto = new LocalizacaoEnderecoMontadoDto()
                    {
                        LocalizacaoId = local.LocalizacaoId,
                        Endereco = local.MontarEndereco()
                    };

                    produtoDto.Localizacoes.Add(localizacaoDto);
                }

                pedidoDto.Produtos.Add(produtoDto);
            }

            pedidosDto.Add(pedidoDto);
        }

        return pedidosDto; 
    }

    public async Task<List<PedidoDto>> ListasPedidosSeparacao()
    {
        var pedidosDto = new List<PedidoDto>();

        var pedidos = await _vendasContext.ListarPedidos();

        foreach (var pedido in pedidos)
        {
            var pedidoDto = new PedidoDto
            {
                Id = pedido.Id,
                Numero = pedido.Numero,
                Vendedor = pedido.Vendedor,
                Status = pedido.Status,
                Criacao = pedido.Criacao,
                Conclusao = pedido.Conclusao,
                Produtos = new List<ProdutoListarDto>()
            };

            foreach (var produto in pedido.Produtos)
            {
                var produtoDto = new ProdutoListarDto()
                {
                    ProdutoId = produto.ProdutoId,
                    Ativo = produto.Ativo,
                    Nome = produto.Nome,
                    Quantidade = produto.Quantidade,
                    Preco = produto.Preco,
                    CodigoBarras = produto.CodigoBarras,
                    Imagem = await _arquivosService.RecuperarImagemProduto(produto.ImagemNome, produto.ImagemTipo),
                    Criacao = produto.Criacao,
                    Atualizacao = produto.Atualizacao,
                    UnidadeMedida = new UnidadeMedidaDto() { UnidadeMedidaId = produto.UnidadeMedida.UnidadeMedidaId, Descricao = produto.UnidadeMedida.Descricao },
                    Localizacoes = new List<LocalizacaoEnderecoMontadoDto>()
                };

                foreach (var local in produto.Localizacoes)
                {
                    var localizacaoDto = new LocalizacaoEnderecoMontadoDto()
                    {
                        LocalizacaoId = local.LocalizacaoId,
                        Endereco = local.MontarEndereco()
                    };

                    produtoDto.Localizacoes.Add(localizacaoDto);
                }

                pedidoDto.Produtos.Add(produtoDto);
            }

            pedidosDto.Add(pedidoDto);
        }

        return pedidosDto;
    }
}
