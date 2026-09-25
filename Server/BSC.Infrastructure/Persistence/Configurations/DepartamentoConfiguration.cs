using BSC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Infrastructure.Persistence.Configurations
{
    public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            // Tabla
            builder.ToTable("departamentos");
            //Primary Key
            builder.HasKey(d => d.IdDepartamento);

            builder.Property(d => d.IdDepartamento)
                .HasColumnName("id_departamento")
                .ValueGeneratedOnAdd();
            // Nombre cliente
            builder.Property(d => d.NombreDepartamento)
                .HasColumnName("nombre_departamento")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
