using BSC.Application.Identity.Login;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BSC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginCasoUso _loginCasoUso;
        public AuthController(LoginCasoUso loginCasoUso) 
        { 
            _loginCasoUso = loginCasoUso;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            var result = await _loginCasoUso.Ejecutar(request.Email, request.Password);

            return Ok(result);
        }
    }
}
