using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Validacoes.Mensagens
{
    public static class MensagensConvite
    {
        public const string USUARIO_REMETENTE_INVALIDO = "O usuário remetente deve ser informado";
        public const string USUARIO_DESTINATARIO_INVALIDO = "O usuário destinatário deve ser informado";
        public const string USUARIO_SEM_PERMISSAO = "O usuário não possui permissão para convidar outros usuários para a conta.";
        public const string CONTA_NAO_ATIVA = "A conta não está ativa.";
    }
}
