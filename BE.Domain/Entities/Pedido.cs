using BE.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Entities;

public class Pedido
{
    public int Numero { get; private set; }
    public string Vendedor { get; private set; }
    public StatusPedido Status { get; private set; }
    public DateTime Criacao { get; private set; }
    public DateTime Conclusao { get; private set; }
    public List<Produto> Produtos { get; private set; }

    public Pedido(int numero, string vendedor, StatusPedido status, DateTime criacao, DateTime conclusao, List<Produto> produtos)
    {
        Numero = numero;
        Vendedor = vendedor;
        Status = status;
        Criacao = criacao;
        Conclusao = conclusao;
        Produtos = produtos;
    }
}

