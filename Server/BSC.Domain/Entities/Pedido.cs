using BSC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Domain.Entities
{
    public class Pedido
    {
        public long IdPedido { get; private set; }
        public long IdCliente { get; private set; }
        public Cliente Cliente { get; private set; } = null!;
        public long IdEmpleado { get; private set; }
        public Empleado Empleado { get; private set; } = null!;
        public DateTime Fecha { get; private set; }
        public EstadoPedido Estado { get; private set; }
        private readonly List<PedidoDetalle> _pedidoDetalles = new();
        public IReadOnlyCollection<PedidoDetalle> PedidoDetalles => _pedidoDetalles.AsReadOnly();
        private Pedido() { }
        public Pedido(long idCliente, long idEmpleado) 
        {
            if (idCliente <= 0)
                throw new ArgumentException(
                    "El cliente es obligatorio.");

            if (idEmpleado <= 0)
                throw new ArgumentException(
                    "El empleado es obligatorio.");

            IdCliente = idCliente;
            IdEmpleado = idEmpleado;

            Fecha = DateTime.UtcNow;
            Estado = EstadoPedido.Pendiente;
        }

        public void AgregarDetalle(Producto producto, int cantidad)
        {
            // si es diferente de estado pendiente
            if (Estado != EstadoPedido.Pendiente)
                throw new InvalidOperationException("Solo se pueden modificar pedidos pendientes.");

            // verificar que no pasen nulos y sea tipo object
            if (producto is null)
                throw new ArgumentNullException(nameof(producto));

            // si no esta activo
            if (!producto.Activo)
                throw new InvalidOperationException($"El producto {producto.NombreProducto} no está activo.");

            // Si el producto ya está en el pedido, se suma la cantidad
            var existeProducto = _pedidoDetalles.FirstOrDefault(d => d.IdProducto == producto.IdProducto);

            // si el producto existe aumenta la cantidad
            if (existeProducto is not null)
                existeProducto.AumentarCantidad(cantidad);
            // si no crea un nuevo producto con cantidad
            else
                _pedidoDetalles.Add(new PedidoDetalle(producto.IdProducto, cantidad));

            // Valida existencia y la descuenta para que no se pueda seleecionar dos veces
            producto.DescontarExistencia(cantidad);
        }
        public void Confirmar()
        {
            // si es diferente de pendiente 
            if (Estado != EstadoPedido.Pendiente)
                throw new InvalidOperationException("Solo se puede confirmar un pedido pendiente.");
            // si el pedido no contiene productos
            if (!_pedidoDetalles.Any())
                throw new InvalidOperationException("El pedido debe tener al menos un producto.");

            Estado = EstadoPedido.Confirmado;
        }

        public void Cancelar()
        {
            if (Estado == EstadoPedido.Cancelado)
                throw new InvalidOperationException("El pedido ya está cancelado.");

            Estado = EstadoPedido.Cancelado;
        }
    }
}