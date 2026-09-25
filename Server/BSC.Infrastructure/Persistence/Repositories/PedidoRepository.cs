using BSC.Application.Interfaces;
using BSC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Infrastructure.Persistence.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly BscDbContext _context;

        public PedidoRepository(BscDbContext context) 
        {
            _context = context;
        }

        public async Task AgregarDetalle(PedidoDetalle detalle)
        {
            await _context.PedidosDetalles.AddAsync(detalle);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarPedido(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
