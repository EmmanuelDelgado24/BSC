using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Domain.Enums
{
    public enum EstadoPedido : byte
    {
        Pendiente = 1,
        Confirmado = 2,
        Cancelado = 3,
        Entregado = 4
    }
}
