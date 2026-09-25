using BSC.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Application.Identity.CrearUsuario
{
    public class CrearUsuarioCasoUso
    {
        private readonly IIdentityService _identityService;

        public CrearUsuarioCasoUso(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<string> Ejecutar(string email, string password)
        {
            return await _identityService.CrearUsuario(email, password);
        }
    }
}
