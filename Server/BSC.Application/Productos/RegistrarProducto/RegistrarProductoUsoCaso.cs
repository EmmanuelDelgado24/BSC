using BSC.Application.Interfaces;
using BSC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Application.Productos.RegistrarProducto
{
    public class RegistrarProductoUsoCaso
    {
        private readonly IProductoRepository _productoRepository;

        public RegistrarProductoUsoCaso(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<long> Ejecutar(string claveProducto, string nombreProducto, int existencia)
        {
            var producto = new Producto(claveProducto, nombreProducto, existencia);

            await _productoRepository.Agregar(producto);

            return producto.IdProducto;
        }
    }
}
