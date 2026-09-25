using BSC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Infrastructure.Persistence.Configurations
{
    public class PedidoDetalleConfiguration : IEntityTypeConfiguration<PedidoDetalle>
    {
        public void Configure(EntityTypeBuilder<PedidoDetalle> builder)
        {
            // Tabla
            builder.ToTable("pedidos_detalles");
            //Primary Key
            builder.HasKey(pd => pd.IdPedidoDetalle);

            builder.Property(pd => pd.IdPedidoDetalle)
                .HasColumnName("id_pedido_detalle")
                .ValueGeneratedOnAdd();
            // Cantidad
            builder.Property(pd => pd.Cantidad)
                .HasColumnName("cantidad")
                .IsRequired();

            // RELACIÓN PEDIDO DETALLE - PRODUCTO
            builder.HasOne(pd => pd.Producto)
                .WithMany(p => p.PedidoDetalles)
                .HasForeignKey(pd => pd.IdProducto)
                .IsRequired();

            // RELACIÓN PEDIDO DETALLE - PEDIDO
            builder.HasOne(pd => pd.Pedido)
                .WithMany(p => p.PedidoDetalles)   // colección en Pedido
                .HasForeignKey(pd => pd.IdPedido)
                .IsRequired();
        }
    }
}
