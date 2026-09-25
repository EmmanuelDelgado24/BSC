using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Infrastructure.Identity
{
    public class Roles
    {
        public static async Task InicializarRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Administrador", "Personal Adminstrativo", "Vendedor" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var resultado = await roleManager.CreateAsync(
                        new IdentityRole(roleName));

                    if (!resultado.Succeeded)
                    {
                        var errores = string.Join(
                            ", ",
                            resultado.Errors.Select(e => e.Description));

                        throw new InvalidOperationException(
                            $"Error creando el rol {roleName}: {errores}");
                    }
                }
            }
        }
    }
}
