using BSC.Application.Identity.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<string> CrearUsuario(string email, string password);
        Task<string> AsignarRol(string email, string rol);
        Task<Login> Login(string email, string password);
    }
}
