﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Validacoes.Mensagens
{
    public static class MensagensBase
    {
        public const string ID_IGUAL_MENOR_ZERO = "Id não pode ser menor que zero";
        public const string DATA_REGISTRO_INVALIDA = "Deve ser registrada a data atual, esta não pode ser manipulada.";
        public const string USUARIO_NAO_INFORMADO = "Usuário não informado!";
    }
}
