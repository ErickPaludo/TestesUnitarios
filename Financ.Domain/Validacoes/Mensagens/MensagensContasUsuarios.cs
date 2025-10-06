using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Validacoes.Mensagens
{
    public static class MensagensContasUsuarios
    {
        public const string IDCONTA_IGUAL_MENOR_ZERO = "IdConta não pode ser menor que zero";
        public const string IDUSUARIO_VAZIO = "IdUsuario não deve ser vazio!";
        public const string ACESSO_INVALIDO = "O acesso informado é inválido.";
    }
}
