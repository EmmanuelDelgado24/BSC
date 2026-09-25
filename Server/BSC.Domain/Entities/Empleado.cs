using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Domain.Entities
{
    public class Empleado
    {
        public long IdEmpleado { get; private set; }
        public string NombreEmpleado { get; private set; } = string.Empty;
        public bool Activo { get; private set; }
        public long IdDepartamento { get; private set; }
        public Departamento Departamento { get; private set; } = null!;
        public string? ApplicationUserId { get; private set; } = string.Empty;

        private Empleado() { }

        public Empleado(string nombreEmpleado, long idDepartamento)
        {
            if (string.IsNullOrWhiteSpace(nombreEmpleado))
                throw new ArgumentNullException(
                    "El nombre del epleado es obligatorio");

            if (idDepartamento <= 0)
                throw new ArgumentException(
                    "El departamento es obligatorio.");

            NombreEmpleado = nombreEmpleado;
            IdDepartamento = idDepartamento;
            Activo = true;

        }

        public void AsignarUsuario(string applicationUserId)
        {
            if (string.IsNullOrWhiteSpace(applicationUserId))
                throw new ArgumentException(
                    "El usuario es obligatorio.");

            ApplicationUserId = applicationUserId;
        }
        public void Desactivar()
        {
            Activo = false;
        }
    }
}