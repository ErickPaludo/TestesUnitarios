using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Validacoes
{
    public class ValidacaoDominio : Exception
    {
        public ValidacaoDominio(string Erro) : base(Erro){}
        public static void VerificaExcessao(bool condicao, string mensagem)
        {
            if (condicao)
                throw new ValidacaoDominio(mensagem);
        }
    }
}
