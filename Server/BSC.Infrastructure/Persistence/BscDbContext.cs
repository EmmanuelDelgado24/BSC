using BSC.Domain.Entities;
using BSC.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Infrastructure.Persistence
{
    public class BscDbContext : IdentityDbContext<ApplicationUser>
    {
        public BscDbContext(DbContextOptions<BscDbContext> options) : base(options)
        { 
        }
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Departamento> Departamentos => Set<Departamento>();
        public DbSet<Empleado> Empleados => Set<Empleado>();
        public DbSet<Pedido> Pedidos => Set<Pedido>();
        public DbSet<PedidoDetalle> PedidosDetalles => Set<PedidoDetalle>();
        public DbSet<Producto> Productos => Set<Producto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BscDbContext).Assembly);
        }
    }
}