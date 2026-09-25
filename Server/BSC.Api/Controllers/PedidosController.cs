using BSC.Application.Pedidos.CrearPedido;
using BSC.Application.Productos.RegistrarProducto;
using BSC.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BSC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly CrearPedidoUsoCaso _crearPedidoUsoCaso;
        public PedidosController(CrearPedidoUsoCaso crearPedidoUsoCaso)
        {
            _crearPedidoUsoCaso = crearPedidoUsoCaso;
        }

        [HttpPost("crear-pedido")]
        public async Task<ActionResult> CrearPedido([FromBody] CrearPedidoDTO request)
        {
            var pedidos = await _crearPedidoUsoCaso.Ejecutar(request.NombreProducto, request.IdCliente,
                request.Cantidad, request.IdEmpleado);

            return Ok(new
            {
                id = pedidos,
                mensaje = "Pedido creado correctamente."
            });
        }
    }
}
