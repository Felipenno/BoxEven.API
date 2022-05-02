using BE.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Interfaces.Service;

public interface IUsuarioService
{
    Task<string> CadastrarUsuarioAsync(UsuarioRegistroDto usuario);
    Task<UsuarioDto> LoginAsync(string apelido, string senha);
    Task<bool> AtualizarUsuarioAsync(UsuarioDto usuario);
    Task<UsuarioDto> ListarUsuarioPorIdAsync(string id);
    Task<bool> VerificarCodigo(string codigo);
    Task<bool> AlterarSenha(string senha);
    Task<bool> ConfirmarCadastro();
}