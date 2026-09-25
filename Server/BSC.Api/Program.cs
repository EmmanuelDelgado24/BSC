using BSC.Application.Identity.AsignarRol;
using BSC.Application.Identity.CrearUsuario;
using BSC.Application.Identity.Login;
using BSC.Application.Interfaces;
using BSC.Application.Pedidos.CrearPedido;
using BSC.Application.Productos.RegistrarProducto;
using BSC.Infrastructure.Identity;
using BSC.Infrastructure.Persistence;
using BSC.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Interfaces
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IProductoRepository,ProductoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

// Casos de Uso
builder.Services.AddScoped<CrearUsuarioCasoUso>();
builder.Services.AddScoped<AsignarRolCasoUso>();
builder.Services.AddScoped<RegistrarProductoUsoCaso>();
builder.Services.AddScoped<CrearPedidoUsoCaso>();
builder.Services.AddScoped<LoginCasoUso>();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
        options.Password.RequiredLength = 8;

        options.Password.RequireDigit = true;

        options.Password.RequireLowercase = true;

        options.Password.RequireUppercase = true;

        options.Password.RequireNonAlphanumeric = false;

        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BscDbContext>()
    .AddSignInManager();

builder.Services.AddDbContext<BscDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration
            .GetConnectionString("BscDatabase"));
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

    await Roles.InicializarRoles(roleManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
