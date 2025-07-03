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
// Asegúrate de tener esto arriba

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Acceso/Login";
        options.AccessDeniedPath = "/Acceso/Denegado"; // opcional, si tienes página de acceso denegado
    });

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();

    // Aplicar migraciones pendientes
    context.Database.EnsureCreated(); // o context.Database.Migrate();

    // Verificar si ya hay usuarios
    if (!context.usuarios.Any())
    {
        // Crear roles si no existen
        if (!context.Roles.Any())
        {
            context.Roles.AddRange(
                new Rol { nomRol = "Administrador", Descripcion = "Acceso total" },
                new Rol { nomRol = "Empleado", Descripcion = "Acceso limitado" }
            );
            context.SaveChanges();
        }

        var hasher = new PasswordHasher<Usuario>();


        // Empleados relacionados
        var adminEmpleado = new Empleado
        {
            nomEmpleado = "Admin",
            apeEmpleado = "Sistema",
            dni = "12345678",
            gmail = "admin@correo.com",
            numCelular = "999999999"
        };

        var empleadoEmpleado = new Empleado
        {
            nomEmpleado = "Empleado",
            apeEmpleado = "Demo",
            dni = "87654321",
            gmail = "empleado@correo.com",
            numCelular = "988888888"
        };

        context.empleados.AddRange(adminEmpleado, empleadoEmpleado);
        context.SaveChanges();

        var usuarioAdmin = new Usuario
        {
            usuario = "admin",
            password = hasher.HashPassword(null, "Admin123"),
            IdEmpleado = adminEmpleado.IdEmpleado
        };

        var usuarioEmpleado = new Usuario
        {
            usuario = "empleado",
            password = hasher.HashPassword(null, "Empleado123"),
            IdEmpleado = empleadoEmpleado.IdEmpleado
        };

        context.usuarios.AddRange(usuarioAdmin, usuarioEmpleado);
        context.SaveChanges();

        // Asignar roles
        var rolAdmin = context.Roles.First(r => r.nomRol == "Administrador");
        var rolEmpleado = context.Roles.First(r => r.nomRol == "Empleado");

        context.UserRoles.AddRange(
            new User_Rol { IdUsuario = usuarioAdmin.IdUsuario, IdRol = rolAdmin.IdRol },
            new User_Rol { IdUsuario = usuarioEmpleado.IdUsuario, IdRol = rolEmpleado.IdRol }
        );

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
