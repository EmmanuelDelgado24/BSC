using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Domain.Entities
{
    public class Cliente
    {
        public long IdCliente { get; private set; }
        public string NombreCliente { get; private set; } = string.Empty;
        public bool Activo { get; private set; }

        private Cliente() { }
    
        public Cliente(string nombreCliente)
        {
            if (string.IsNullOrWhiteSpace(nombreCliente))
                throw new ArgumentNullException(
                    "El nombre del cliente es obligatorio.");

            NombreCliente = nombreCliente;
            Activo = true;
        }
        public void Desactivar()
        {
            Activo = false;
        }
    }
}