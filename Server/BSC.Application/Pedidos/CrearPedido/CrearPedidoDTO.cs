using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Application.Pedidos.CrearPedido
{
    public class CrearPedidoDTO
    {
        public string NombreProducto { get; set; } = string.Empty;
        public long IdCliente { get; set; }
        public int Cantidad { get; set; }
        public long IdEmpleado { get; set; }
    }
}
