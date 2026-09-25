using BSC.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Application.Identity.Login
{
    public class LoginCasoUso
    {
        private readonly IIdentityService _identityService;

        public LoginCasoUso(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Login> Ejecutar(string email, string password)
        {
            return await _identityService.Login(email, password);

        }
    }
}