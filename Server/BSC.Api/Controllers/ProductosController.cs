using BSC.Application.Identity.CrearUsuario;
using BSC.Application.Interfaces;
using BSC.Application.Productos.RegistrarProducto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BSC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly RegistrarProductoUsoCaso _registrarProductoUsoCaso;

        public ProductosController(RegistrarProductoUsoCaso registrarProductoUsoCaso)
        {
            _registrarProductoUsoCaso = registrarProductoUsoCaso;
        }

        [HttpPost("crear-producto")]
        public async Task<IActionResult> CrearProducto([FromBody] RegistrarProductoDTO request)
        {
            var productos = await _registrarProductoUsoCaso.Ejecutar(request.ClaveProducto, 
                                              request.NombreProducto, request.Existencia);

            return Ok(new
            {
                id = productos,
                mensaje = "Producto creado correctamente."
            });

        }
    }
}
