using BE.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Interfaces.Service;

public interface IUsuarioService
{
    Task<bool> NovoUsuarioAsync(UsuarioDto usuario);
    Task<bool> AtualizarUsuarioAsync(UsuarioDto usuario);
    Task<UsuarioDto> ListarUsuarioPorIdAsync(int id);
    Task<bool> VerificarCodigo(string codigo);
    Task<bool> AlterarSenha(string senha);
    Task<bool> ConfirmarCadastro();
}