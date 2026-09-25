using BSC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Application.Interfaces
{
    public interface IPedidoRepository
    {
        Task AgregarDetalle(PedidoDetalle detalle);
        Task AgregarPedido(Pedido pedido);
    }
}