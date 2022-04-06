using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Dtos;

public class UsuarioLoginDto
{
    [Required]
    public string Apelido { get; set; }
    [Required]
    public string Senha { get; set; }
}

