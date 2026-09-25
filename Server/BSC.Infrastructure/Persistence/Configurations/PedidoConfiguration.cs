using BSC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Infrastructure.Persistence.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            // Tabla
            builder.ToTable("pedidos");
            // Primary Key
            builder.HasKey(p => p.IdPedido);

            builder.Property(p => p.IdPedido)
                .HasColumnName("id_pedido")
                .ValueGeneratedOnAdd();
            // ID CLiente
            builder.Property(p => p.IdCliente)
                .HasColumnName("id_cliente")
                .IsRequired();
            // ID Empleado
            builder.Property(p => p.IdEmpleado)
                .HasColumnName("id_empleado")
                .IsRequired();
            // Fecha
            builder.Property(p => p.Fecha)
               .HasColumnName("Fecha")
               .IsRequired();
            // Estado
            builder.Property(p => p.Estado)
               .HasColumnName("Estado")
               .IsRequired();

            // RELACIÓN PEDIDO - EMPLEADO

            builder.HasOne(e => e.Empleado)
                .WithMany()
                .HasForeignKey(e => e.IdEmpleado)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN PEDIDO - CLIENTE

            builder.HasOne(e => e.Cliente)
               .WithMany()
               .HasForeignKey(e => e.IdCliente)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(p => p.PedidoDetalles)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
