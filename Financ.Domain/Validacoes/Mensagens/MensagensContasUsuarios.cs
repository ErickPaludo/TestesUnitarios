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
        public const string ACESSO_NEGADO = "O usuário não é um administrador!";
        public const string USUARIO_MESTRE_NAO_PODE_SER_ATUALIZADO = "Não é possível alterar um usuário mestre!";
        public const string USUARIO_INATIVO_NAO_PODE_SER_ATUALIZADO = "O usuário não está ativo!";
    }
}
