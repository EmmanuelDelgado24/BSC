using BSC.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Application.Identity.AsignarRol
{
    public class AsignarRolCasoUso 
    {
        private readonly IIdentityService _identityService;

        public AsignarRolCasoUso(IIdentityService identityService) 
        {
            _identityService = identityService;
        }
        public async Task<string> Ejecutar(string email, string rol)
        {
            return await _identityService
                .AsignarRol(email, rol);
        }
    }
}
