using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Validacoes
{
    public static class MensagensDominio
    {
        #region ContaBase
        public const string ID_IGUAL_MENOR_ZERO = "Id não pode ser menor que zero";
        public const string DATA_REGISTRO_INVALIDA = "Deve ser registrada a data atual, esta não pode ser manipulada.";
        #endregion

        #region Conta
        public const string TITULO_OBRIGATORIO = "O título é obrigatório.";
        public const string TITULO_TAMANHO_INVALIDO = "O título deve possuir entre 5 e 100 caracteres.";
        public const string TIPO_CONTA_INVALIDO = "Tipo de conta inválido.";
        public const string STATUS_INVALIDO = "Status inválido.";
        public const string FECHAMENTO_INVALIDO = "Dia de fechamento inválido, deve estar entre 1 e 16.";
        public const string VENCIMENTO_MENOR_FECHAMENTO = "O vencimento deve ser maior que o fechamento.";
        public const string VENCIMENTO_MINIMO_7_DIAS = "O vencimento deve ter pelo menos 7 dias após o fechamento.";
        public const string VENCIMENTO_MAXIMO_12_DIAS = "O vencimento deve ter no máximo 12 dias após o fechamento.";
        public const string CREDITO_MENOR_QUE_ZERO = "O Crédito informado deve ser maior que zero!";
        #endregion

        #region Contas Usuários
        public const string IDCONTA_IGUAL_MENOR_ZERO = "IdConta não pode ser menor que zero";
        public const string IDUSUARIO_IGUAL_MENOR_ZERO = "IdUsuario não pode ser menor que zero";
        public const string ACESSO_INVALIDO = "O acesso informado é inválido.";
        #endregion
    }
}
