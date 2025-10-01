using Financ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.DTOs.ContasUsuarios
{
    public class InclusaoContaUsuarioDTO
    {
        [Required(ErrorMessage = "Informe uma conta!")]
        public int IdConta { get; set; }
        [Required(ErrorMessage = "Informe o tipo de acesso do usuário!")]
        public TiposAcessos Acesso { get; set; } = TiposAcessos.Visualizador;
    }
}
