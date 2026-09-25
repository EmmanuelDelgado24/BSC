using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Domain.Entities
{
    public class Departamento
    {
        public long IdDepartamento { get; private set; }
        public string NombreDepartamento { get; private set; } = string.Empty;

        private Departamento() { }

        public Departamento(string nombreDepartamento)
        {
            if (string.IsNullOrWhiteSpace(nombreDepartamento))
                throw new ArgumentNullException(
                    "El nombre de departamento es obligatorio.");

            NombreDepartamento = nombreDepartamento;

        }
    }
}
