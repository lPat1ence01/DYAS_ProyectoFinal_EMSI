using EMSI_Corporation.Data;
using EMSI_Corporation.Models;
using EMSI_Corporation.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using OfficeOpenXml;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace EMSI_Corporation.Controllers
{
    [Authorize] // Esto asegura que solo usuarios autenticados puedan acceder al resto de acciones
    public class AccesoController : Controller
    {
        private readonly AppDBContext _appDBContext;
        private readonly PasswordHasher<Usuario> _passwordHasher;
        //prueba
        public AccesoController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Acceso");
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var usuario = await _appDBContext.usuarios
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.usuario.Trim().ToLower() == modelo.Username.Trim().ToLower());

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "El usuario no existe.");
                return View(modelo);
            }

            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.password, modelo.Password);

            if (resultado == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Contraseña incorrecta.");
                return View(modelo);
            }

            var rolUsuario = usuario.UserRoles.FirstOrDefault()?.Rol?.nomRol ?? "Empleado";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.usuario),
                new Claim(ClaimTypes.Role, rolUsuario)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return RedirectToAction("Index", "Home"); // Cambia a donde quieras redirigir después del login
        }



        [HttpGet]
        public IActionResult Mantenimiento()
        {
            return View();
        }
        [HttpGet]
        public IActionResult PopupMant()
        {
            ViewBag.Empresas = _appDBContext.Clientes.ToList();
            ViewBag.Trabajadores = _appDBContext.empleados.ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Stakeholders()
        {
            return View();
        }

        // Listar
        [HttpGet]
        public IActionResult Stakeholders_Cliente()
        {
            var lista = _appDBContext.Clientes.ToList();
            return View(lista);
        }

        // Crear
        [HttpGet]
        public IActionResult CrearCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearCliente(Cliente cliente)
        {
            _appDBContext.Clientes.Add(cliente);
            _appDBContext.SaveChanges();
            return RedirectToAction(nameof(Stakeholders_Cliente));
        }

        // Editar
        [HttpGet]
        public IActionResult EditarCliente(int id)
        {
            var cliente = _appDBContext.Clientes.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public IActionResult EditarCliente(Cliente cliente)
        {
            _appDBContext.Clientes.Update(cliente);
            _appDBContext.SaveChanges();
            return RedirectToAction(nameof(Stakeholders_Cliente));
        }

        // Eliminar
        [HttpGet]
        public IActionResult EliminarCliente(int id)
        {
            var cliente = _appDBContext.Clientes.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public IActionResult ConfirmarEliminarCliente(Cliente cliente)
        {
            var entidad = _appDBContext.Clientes.Find(cliente.IdCliente);
            if (entidad != null)
            {
                _appDBContext.Clientes.Remove(entidad);
                _appDBContext.SaveChanges();
            }
            return RedirectToAction(nameof(Stakeholders_Cliente));
        }

        [HttpGet]
        public IActionResult Stakeholders_Proovedor()
        {
            var lista = _appDBContext.Provedor?.ToList();
            return View(lista);
        }

        //Crear
        [HttpGet]
        public IActionResult CrearProovedor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearProovedor(Proovedor proveedor)
        {
            _appDBContext.Provedor.Add(proveedor);
            _appDBContext.SaveChanges();
            return RedirectToAction(nameof(Stakeholders_Proovedor));
        }

        //Editar
        [HttpGet]
        public IActionResult EditarProovedor(int id)
        {
            var proveedor = _appDBContext.Provedor.Find(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }

        [HttpPost]
        public IActionResult EditarProovedor(Proovedor proveedor)
        {
            _appDBContext.Provedor.Update(proveedor);
            _appDBContext.SaveChanges();
            return RedirectToAction(nameof(Stakeholders_Proovedor));
        }

        //Eliminar
        [HttpGet]
        public IActionResult EliminarProovedor(int id)
        {
            var proveedor = _appDBContext.Provedor.Find(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }

        [HttpPost]
        public IActionResult ConfirmarEliminarProovedor(Proovedor proveedor)
        {
            var entidad = _appDBContext.Provedor.Find(proveedor.IdProovedor);
            if (entidad != null)
            {
                _appDBContext.Provedor.Remove(entidad);
                _appDBContext.SaveChanges();
            }
            return RedirectToAction(nameof(Stakeholders_Proovedor));
        }

        [HttpGet]
        public IActionResult Stakeholders_Empleado()
        {
            var lista = _appDBContext.empleados?.ToList();
            return View(lista);
        }

        //Crear
        [HttpGet]
        public IActionResult CrearEmpleado()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearEmpleado(IFormCollection form)
        {
            // Captura datos del formulario
            var empleado = new Empleado
            {
                nomEmpleado = form["nomEmpleado"],
                apeEmpleado = form["apeEmpleado"],
                dni = form["dni"],
                gmail = form["gmail"],
                numCelular = form["numCelular"]
            };

            _appDBContext.empleados.Add(empleado);
            _appDBContext.SaveChanges();

            // Crea el usuario asociado
            var usuario = new Usuario
            {
                usuario = form["usuario"],
                password = new PasswordHasher<Usuario>().HashPassword(null, form["password"]),
                IdEmpleado = empleado.IdEmpleado
            };

            _appDBContext.usuarios.Add(usuario);
            _appDBContext.SaveChanges();

            // Asignar el rol automáticamente (Empleado)
            var rolEmpleado = _appDBContext.Roles.FirstOrDefault(r => r.nomRol == "Empleado");
            if (rolEmpleado != null)
            {
                _appDBContext.UserRoles.Add(new User_Rol
                {
                    IdUsuario = usuario.IdUsuario,
                    IdRol = rolEmpleado.IdRol
                });
                _appDBContext.SaveChanges();
            }

            TempData["Success"] = "Empleado registrado correctamente.";
            return RedirectToAction("Stakeholders_Empleado");
        }


        //Editar
        [HttpGet]
        public IActionResult EditarEmpleado(int id)
        {
            var empleado = _appDBContext.empleados.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        [HttpPost]
        public IActionResult EditarEmpleado(Empleado empleado)
        {
            _appDBContext.empleados.Update(empleado);
            _appDBContext.SaveChanges();
            return RedirectToAction(nameof(Stakeholders_Empleado));
        }

        //Eliminar
        [HttpGet]
        public IActionResult EliminarEmpleado(int id)
        {
            var empleado = _appDBContext.empleados.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        [HttpPost]
        public IActionResult ConfirmarEliminarEmpleado(Empleado empleado)
        {
            var entidad = _appDBContext.empleados.Find(empleado.IdEmpleado);
            if (entidad != null)
            {
                _appDBContext.empleados.Remove(entidad);
                _appDBContext.SaveChanges();
            }
            return RedirectToAction(nameof(Stakeholders_Empleado));
        }

        [HttpPost]
        public IActionResult GoToMant(ClienteTrabajadorVM cliente_trabajador)
        {
            /*using var workbook = new XLWorkbook();
            var worksheet = workbook.AddWorksheet("Sample Sheet");
            worksheet.Cell("A1").Value = "Hello World 2!";
            worksheet.Cell("A2").FormulaA1 = "MID(A1, 7, 5)";
            workbook.Save();*/

            return View("Mantenimiento", cliente_trabajador);
        }

        [HttpGet]
        public FileResult DescargarExcel()
        {
            ExcelPackage.License.SetNonCommercialPersonal("EMSI");

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Datos");

                worksheet.Row(1).Style.Fill.SetBackground(System.Drawing.Color.AliceBlue);
                worksheet.Cells[1, 1, 1, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black, true);
                worksheet.Row(1).CustomHeight = true;
                worksheet.Row(1).Height = 50;

                // Agrega datos
                worksheet.Cells[1, 1].Value = "Nombre";
                worksheet.Cells[1, 2].Value = "Edad";

                worksheet.Cells[2, 1].Value = "Julissa";
                worksheet.Cells[2, 2].Value = 25;

                var stream = new MemoryStream(package.GetAsByteArray());

                return File(stream.ToArray(),
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "reporte.xlsx");
            }
        }

        /*
         //Este funciona pero descarga solo a nivel de backend, es decir, no lo descarga en la computadora del frontend, se guarda el excel dentro de la solución
        [HttpGet]
        public void DownloadFile()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.AddWorksheet("Sample Sheet");
            worksheet.Cell("A1").Value = "Hello World!";
            worksheet.Cell("A2").FormulaA1 = "MID(A1, 7, 5)";
            workbook.SaveAs("HelloWorld.xlsx");
            
        }*/


        /*
        [HttpPost]
        public IActionResult UploadNewProd()//falta VM
        {
            //return View("Mantenimiento");
        }*/
    }
}
