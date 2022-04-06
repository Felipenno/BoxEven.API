using BE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Interfaces.Service;

public interface IPedidoService
{
    Task<List<Pedido>> ListasPedidosSeparacao();
    Task<bool> AtualizarStatus();
}

