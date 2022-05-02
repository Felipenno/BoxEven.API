using BE.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Interfaces.Service;

public interface IMovimentacaoService
{
    Task<bool> CriarMovimentacaoAsync(MovimentacaoCriarDto movimentacao);
    Task<MovimentacaoDto> ListarPorIdAsync(int id);
    Task<List<MovimentacaoDto>> ListarMovimentacaoesAsync();
}
