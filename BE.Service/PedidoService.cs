using BE.Domain.Entities;
using BE.Domain.Enum;
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
    private readonly IVendasContext _vendasContext;

    public PedidoService(IVendasContext vendasContext)
    {
        _vendasContext = vendasContext;
    }

    public async Task AtualizarStatus(string id, StatusPedido status)
    {
       await _vendasContext.AlterarStatusPedidoAsync(id, status);
    }

    public async Task<List<Pedido>> ListarPedidosPorFiltroAsync(StatusPedido status, DateTime conclusao)
    {
        return await _vendasContext.ListarPedidosPorFiltroAsync(status, conclusao);
    }

    public async Task<List<Pedido>> ListasPedidosSeparacao()
    {
        return await _vendasContext.ListarPedidos();
    }
}

