using BE.Domain.Dtos;
using BE.Domain.Entities;
using BE.Domain.Enum;

namespace BE.Domain.Interfaces.Service;

public interface IPedidoService
{
    Task<List<PedidoDto>> ListasPedidosSeparacao();
    Task<List<PedidoDto>> ListarPedidosPorFiltroAsync(StatusPedido status, DateTime conclusao);
    Task AtualizarStatus(string id, StatusPedido status);
}
