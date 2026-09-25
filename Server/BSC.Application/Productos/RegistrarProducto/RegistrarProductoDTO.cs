using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Application.Productos.RegistrarProducto
{
    public class RegistrarProductoDTO
    {
        public string ClaveProducto { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public int Existencia { get; set; }
    }
}
