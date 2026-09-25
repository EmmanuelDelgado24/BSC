using BSC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Application.Interfaces
{
    public interface IProductoRepository
    {
        Task Agregar(Producto producto);
        Task<IEnumerable<Producto>> BuscarPorNombre(string nombreProducto);
    }
}
