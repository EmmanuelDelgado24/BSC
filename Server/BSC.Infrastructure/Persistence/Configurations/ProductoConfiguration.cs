using BSC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Infrastructure.Persistence.Configurations
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            // Tabla
            builder.ToTable("productos");
            //Primary Key
            builder.HasKey(p => p.IdProducto);

            builder.Property(p => p.IdProducto)
                .HasColumnName("id_producto")
                .ValueGeneratedOnAdd();
            // Clave producto
            builder.Property(p => p.ClaveProducto)
                .HasColumnName("clave_producto")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
            // Nombre
            builder.Property(p => p.NombreProducto)
                .HasColumnName("nombre_producto")
                .HasMaxLength(50)
                .IsRequired();
            // Existencia
            builder.Property(p => p.Existencia)
                .HasColumnName("existencia")
                .IsRequired();
            // Activo
            builder.Property(p => p.Activo)
                .HasColumnName("activo")
                .IsRequired();
            // ClaveProducto no se puede repetir
            builder.HasIndex(p => p.ClaveProducto)
                .IsUnique();
        }
    }
}
