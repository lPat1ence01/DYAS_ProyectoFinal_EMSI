using EMSI_Corporation.Data;
using EMSI_Corporation.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Acceso/Login";
        options.AccessDeniedPath = "/Acceso/Denegado";
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();

    // Aplicar migraciones pendientes
    context.Database.EnsureCreated();

    // Crear roles fijos si no existen
    var rolesFijos = new List<Rol>
    {
        new Rol { nomRol = "Administrador", Descripcion = "Acceso total" },
        new Rol { nomRol = "Supervisor", Descripcion = "Mismas funciones que administrador" },
        new Rol { nomRol = "Atencion cliente", Descripcion = "Acceso a ventas, proveedores, calendario, productos y clientes" },
        new Rol { nomRol = "Almacenero", Descripcion = "Acceso a operaciones, registro de mantenimiento y calendario" }
    };

    foreach (var rol in rolesFijos)
    {
        if (!context.Roles.Any(r => r.nomRol == rol.nomRol))
        {
            context.Roles.Add(rol);
        }
    }
    context.SaveChanges();

    // Crear solo el usuario admin si no hay usuarios
    if (!context.usuarios.Any())
    {
        var hasher = new PasswordHasher<Usuario>();

        var adminEmpleado = new Empleado
        {
            nomEmpleado = "Admin",
            apeEmpleado = "Sistema",
            dni = "12345678",
            gmail = "admin@correo.com",
            numCelular = "999999999"
        };

        context.empleados.Add(adminEmpleado);
        context.SaveChanges();

        var usuarioAdmin = new Usuario
        {
            usuario = "admin",
            password = hasher.HashPassword(null, "Admin123"),
            IdEmpleado = adminEmpleado.IdEmpleado
        };

        context.usuarios.Add(usuarioAdmin);
        context.SaveChanges();

        var rolAdmin = context.Roles.First(r => r.nomRol == "Administrador");

        context.UserRoles.Add(new User_Rol
        {
            IdUsuario = usuarioAdmin.IdUsuario,
            IdRol = rolAdmin.IdRol
        });

        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();
