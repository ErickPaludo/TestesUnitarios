﻿using Financ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Entidades
{
    public abstract class BaseConta
    {
        public int Id { get; protected set; }
        public TiposStatus Status { get; protected set; }
        public DateTime DthrReg { get; protected set; }
    }
}
