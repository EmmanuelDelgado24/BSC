using BSC.Domain.Entities;
using BSC.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Infrastructure.Persistence.Configurations
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            // Tabla
            builder.ToTable("empleados");
            //Primary Key
            builder.HasKey(e => e.IdEmpleado);

            builder.Property(e => e.IdEmpleado)
                .HasColumnName("id_empleado")
                .ValueGeneratedOnAdd();
            // Nombre Empleado
            builder.Property(e => e.NombreEmpleado)
            .HasColumnName("nombre_empleado")
            .HasMaxLength(150)
            .IsRequired();

            builder.Property(e => e.Activo)
                .HasColumnName("activo")
                .IsRequired();

            builder.Property(e => e.IdDepartamento)
                .HasColumnName("id_departamento")
                .IsRequired();

            builder.HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<Empleado>(
                    e => e.ApplicationUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // RELACIÓN EMPLEADO - DEPARTAMENTO

            builder.HasOne(e => e.Departamento)
                .WithMany()
                .HasForeignKey(e => e.IdDepartamento);
        }
    }
}
