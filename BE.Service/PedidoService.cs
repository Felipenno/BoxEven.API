using BE.Domain.Entities;
using BE.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Service;

public class PedidoService : IPedidoService
{
    public Task<bool> AtualizarStatus()
    {
        throw new NotImplementedException();
    }

    public Task<List<Pedido>> ListasPedidosSeparacao()
    {
        throw new NotImplementedException();
    }
}

