using BSC.Application.Interfaces;
using BSC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Application.Pedidos.CrearPedido
{
    public class CrearPedidoUsoCaso
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProductoRepository _productoRepository;
        public CrearPedidoUsoCaso(IPedidoRepository pedidoRepository,
                                  IProductoRepository productoRepository
                                  ) 
        {
            _pedidoRepository = pedidoRepository;
            _productoRepository = productoRepository;
        }

        public async Task<long> Ejecutar(string nombreProducto, long idCliente, int cantidad, long idEmpleado) 
        {
            if (string.IsNullOrWhiteSpace(nombreProducto))
                throw new ArgumentException("El nombre del producto no puede estar vacío.");

            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a 0.");

            var productosEncontrados = await _productoRepository.BuscarPorNombre(nombreProducto);
            var producto = productosEncontrados.FirstOrDefault();

            if (producto == null)
                throw new Exception($"El producto '{nombreProducto}' no existe.");

            var pedido = new Pedido(idCliente, idEmpleado);
            await _pedidoRepository.AgregarPedido(pedido);

            var detalle = new PedidoDetalle(
                producto.IdProducto,
                cantidad
            );
                
            await _pedidoRepository.AgregarDetalle(detalle);

            return pedido.IdPedido;
        }
    }
}
