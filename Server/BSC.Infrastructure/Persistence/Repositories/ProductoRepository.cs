using BSC.Application.Interfaces;
using BSC.Domain.Entities;
using BSC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Infrastructure.Persistence.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly BscDbContext _context;
        public ProductoRepository(BscDbContext context) 
        { 
            _context = context;
        }
        public async Task Agregar(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Producto>> BuscarPorNombre(string nombreProducto)
        {
            if (string.IsNullOrWhiteSpace(nombreProducto))
                return Enumerable.Empty<Producto>();

            return await _context.Productos
                .AsNoTracking()
                .Where(p => p.NombreProducto.Contains(nombreProducto))
                .ToListAsync();
        }
    }
}
