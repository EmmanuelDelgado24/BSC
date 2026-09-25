using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Domain.Entities
{
    public class Producto
    {
        public long IdProducto { get; private set; }
        public string ClaveProducto { get; private set; } = string.Empty;
        public string NombreProducto { get; private set; } = string.Empty;
        public int Existencia { get; private set; }
        public bool Activo { get; private set; }

        public ICollection<PedidoDetalle> PedidoDetalles { get; private set; } = new List<PedidoDetalle>();

        private Producto(){}

        public Producto(string claveProducto, string nombreProducto, int existencia)
        {
            if (string.IsNullOrWhiteSpace(claveProducto))
                throw new ArgumentException(
                    "La clave del producto es obligatoria.");

            if (string.IsNullOrWhiteSpace(nombreProducto))
                throw new ArgumentException(
                    "El nombre es obligatorio.");

            if (existencia < 0)
                throw new ArgumentException(
                    "La existencia no puede ser negativa.");

            ClaveProducto = claveProducto;
            NombreProducto = nombreProducto;
            Existencia = existencia;
            Activo = true;
        }

        //si la cantidad es mayor que la existencia  
        public void DescontarExistencia(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException(
                    "La cantidad debe ser mayor a cero.");

            if (cantidad > Existencia)
                throw new InvalidOperationException(
                    "Existencia insuficiente.");

            Existencia = Existencia - cantidad;
        }

        public void AgregarExistencia(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException(
                    "La cantidad debe ser mayor a cero.");

            Existencia += cantidad;                 
        }

        public void Desactivar()
        {
            Activo = false;
        }
    }
}
