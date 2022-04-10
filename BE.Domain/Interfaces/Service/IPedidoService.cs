using BE.Domain.Entities;
using BE.Domain.Enum;

namespace BE.Domain.Interfaces.Service;

public interface IPedidoService
{
    Task<List<Pedido>> ListasPedidosSeparacao();
    Task<List<Pedido>> ListarPedidosPorFiltroAsync(StatusPedido status, DateTime conclusao);
    Task AtualizarStatus(string id, StatusPedido status);
}
