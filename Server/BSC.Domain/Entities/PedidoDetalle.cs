using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Domain.Entities
{
    public class PedidoDetalle
    {
        public long IdPedidoDetalle { get; private set; }
        public long IdPedido { get; private set; }
        public Pedido Pedido { get; private set; } = null!;
        public long IdProducto {  get; private set; }
        public Producto Producto { get; private set; } = null!;
        public int Cantidad { get; private set; }

        private PedidoDetalle() { }

        public PedidoDetalle(long idProducto, int cantidad)
        {
            if (idProducto <= 0)
                throw new ArgumentException(
                    "El producto es obligatorio.");

            if (cantidad <= 0)
                throw new ArgumentException(
                    "La cantidad debe ser mayor a cero.");

            IdProducto = idProducto;
            Cantidad = cantidad;
        }
        public void AumentarCantidad(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");
            Cantidad += cantidad;
        }
    }
}