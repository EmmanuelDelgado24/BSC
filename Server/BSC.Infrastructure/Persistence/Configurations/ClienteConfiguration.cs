using BSC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Infrastructure.Persistence.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            // Tabla
            builder.ToTable("clientes");
            //Primary Key
            builder.HasKey(c => c.IdCliente);

            builder.Property(c => c.IdCliente)
               .HasColumnName("id_cliente")
               .ValueGeneratedOnAdd();
            // Nombre cliente
            builder.Property(c => c.NombreCliente)
                .HasColumnName("nombre_cliente")
                .HasMaxLength(50)
                .IsRequired();
            // Activo
            builder.Property(c => c.Activo)
                .HasColumnName("activo")
                .IsRequired();
        }
    }
}
