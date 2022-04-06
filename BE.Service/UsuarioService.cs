using BE.Domain.Dtos;
using BE.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Service;

public class UsuarioService : IUsuarioService
{
    public Task<bool> AlterarSenha(string senha)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AtualizarUsuarioAsync(UsuarioDto usuario)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ConfirmarCadastro()
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioDto> ListarUsuarioPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> NovoUsuarioAsync(UsuarioDto usuario)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VerificarCodigo(string codigo)
    {
        throw new NotImplementedException();
    }
}

