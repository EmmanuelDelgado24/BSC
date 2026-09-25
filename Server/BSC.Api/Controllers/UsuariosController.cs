using BSC.Application.Identity.AsignarRol;
using BSC.Application.Identity.CrearUsuario;
using BSC.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BSC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly CrearUsuarioCasoUso _crearUsuarioCasoUso;
        private readonly AsignarRolCasoUso _asignarRolCasoUso;

        public UsuariosController(CrearUsuarioCasoUso crearUsuarioCasoUso,
                                  AsignarRolCasoUso asignarRolCasoUso)
        {
            _crearUsuarioCasoUso = crearUsuarioCasoUso;
            _asignarRolCasoUso = asignarRolCasoUso;
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("crear-usuario")]
        public async Task<IActionResult> CrearUsuario([FromBody] CrearUsuarioDTO request)
        {
            var usuario = await _crearUsuarioCasoUso.Ejecutar(request.Email, request.Password);
            return Ok(new
            {
                id = usuario,
                mensaje = "Usuario creado correctamente"
            });
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("asignar-rol")]
        public async Task<IActionResult> AsignarRol([FromBody] AsignarRolDTO request)
        {
            var rol = await _asignarRolCasoUso.Ejecutar(request.Email, request.Rol);
            return Ok(new { rol });
        }
    }
}
