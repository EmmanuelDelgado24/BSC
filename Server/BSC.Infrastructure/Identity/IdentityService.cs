using BSC.Application.Identity.Login;
using BSC.Application.Interfaces;
using BSC.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {   
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IdentityService(UserManager<ApplicationUser> userManager, 
                               RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<string> CrearUsuario(string email, string password)
        {

            var existeUsuario = await _userManager.FindByNameAsync(email);

            if (existeUsuario is not null) 
               throw new InvalidOperationException(
                "Ya existe un usuario con ese correo.");

            var usuario = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };

            var resultado = await _userManager.CreateAsync(usuario, password);

            if (!resultado.Succeeded)
            {
                var errores = string.Join(", ", resultado.Errors.Select(e => e.Description));

                throw new InvalidOperationException(errores);
            }

            return usuario.Id;
        }

        public async Task<string> AsignarRol(string email, string rol)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario is null)
                throw new InvalidOperationException(
                    "El usuario no existe");

            // Asegurarse de que el rol exista
            if (!await _roleManager.RoleExistsAsync(rol))
            {
                var crearRol = await _roleManager.CreateAsync(new IdentityRole(rol));

            }

            // Asignar usuario al rol
            var resultadoAsignacion = await _userManager.AddToRoleAsync(usuario, rol);

            if (!resultadoAsignacion.Succeeded)
            {
                var errores = string.Join(
                    ", ",
                    resultadoAsignacion.Errors.Select(e => e.Description));

                throw new InvalidOperationException(errores);
            }

            return $"Rol {rol} asignado a {email}";
        }

        public async Task<Login> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("Debe proporcionar un email.",
                 nameof(email));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Debe proporcionar una contraseña.",
                nameof(password));

            // Validar email
            var busquedaEmail = await _userManager.FindByEmailAsync(email);

            if (busquedaEmail is null)
                throw new InvalidOperationException(
                    "Usuario o contraseña incorrectos.");

            // Validar contraseña
            var passwordCorrecto = await _userManager.CheckPasswordAsync(busquedaEmail,password);

            if (!passwordCorrecto)
                throw new InvalidOperationException(
                    "Usuario o contraseña incorrectos.");

            // Obtener roles
            var roles = await _userManager.GetRolesAsync(busquedaEmail);


            return new Login
            {
                Email = busquedaEmail.Email ?? string.Empty,
                Roles = roles
            };
        }
    }
}