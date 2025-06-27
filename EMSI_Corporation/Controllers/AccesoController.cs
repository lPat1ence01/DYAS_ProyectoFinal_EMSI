using EMSI_Corporation.Data;
using EMSI_Corporation.Models;
using EMSI_Corporation.ViewModels;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using OfficeOpenXml;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace EMSI_Corporation.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AppDBContext _appDBContext;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public List<MantenimientoVM> ls_mantenimientos = new List<MantenimientoVM>();

        public AccesoController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM modelo)
        {
            var usuario = await _appDBContext.usuarios
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.Correo.Trim().ToLower() == modelo.Correo.Trim().ToLower());

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "El usuario no existe.");
                return View();
            }

            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Contraseña, modelo.Password);

            if (resultado == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Contraseña incorrecta.");
                return View();
            }

            // Suponiendo que un usuario puede tener varios roles, tomamos el primero
            var rolUsuario = usuario.UserRoles.FirstOrDefault()?.Rol?.Nombre ?? "Usuario";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IDUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, rolUsuario)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            if (rolUsuario == "Administrador")
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                return RedirectToAction("Index", "Articulos");
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Mantenimiento()
        {
            return View();
        }

        private IActionResult MantenimientoAgregarLista()
        {
            
            
            
        }

        [HttpPost]
        public IActionResult MantenimientoPOST(MantenimientoPOSTVM mant)
        {
            Cliente? cli = _appDBContext.Clientes.Find(mant.IdCliente);
            if (cli == null) return null;
            Empleado? em = _appDBContext.empleados.Find(mant.IdEmpleado);
            if (em == null) return null;
            ClienteTrabajadorVM cliente_trabajador = new ClienteTrabajadorVM
            {
                cliente = cli,
                trabajador = em//////////////////////////////////////////////////////////
            };

            MantenimientoVM mantenimientoVM = new MantenimientoVM
            {
                AperturaCorrecta = mant.AperturaCorrecta,
                BarometroCorrecto = mant.BarometroCorrecto,
                BoquillaCorrecta = mant.BoquillaCorrecta,
                EstadoIndicador = mant.EstadoIndicador,
                EstadoPrecinto = mant.EstadoPrecinto,
                ExteriorCorrecto = mant.ExteriorCorrecto,
                FechaMantenimiento = mant.FechaMantenimiento,
                id_extintor = mant.IdExtintor,
                InstruccionesVisibles = mant.InstruccionesVisibles,
                LugarAdecuado = mant.LugarAdecuado,
                MangueraCorrecta = mant.MangueraCorrecta,
                PesoCorrecto = mant.PesoCorrecto,
                PresionCorrecta = mant.PresionCorrecta,
                Señalización = mant.Señalización,
                Usado = mant.Usado,
                Visible = mant.Visible,

            };

            for (var i = 0; i < ls_mantenimientos.Count; i++)
            {
                if (ls_mantenimientos[i].id_extintor == mant.IdExtintor)
                {
                    ls_mantenimientos[i] = mantenimientoVM;
                    ViewBag.cliente_trabajador = cliente_trabajador;

                    return View("Mantenimiento");
                }
            }
            this.ls_mantenimientos.Add(mantenimientoVM);
            ViewBag.cliente_trabajador = cliente_trabajador;

            return View("Mantenimiento");

            return MantenimientoAgregarLista();

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

        [HttpGet]
        public IActionResult Stakeholders_Cliente()
        {
            var clientes = _appDBContext.Clientes.ToList();

            return View(clientes);
        }

        [HttpGet]
        public IActionResult Stakeholders_Proovedor()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Agregar_Cliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar_Cliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _appDBContext.Clientes.Add(cliente);
                _appDBContext.SaveChanges();
                return RedirectToAction(nameof(Stakeholders_Cliente));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Editar_Cliente(int id)
        {
            var cliente = _appDBContext.Clientes.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            else
            {
                return View(cliente);
            }
        }
        [HttpPost]
        public IActionResult Editar_Cliente(Cliente cliente)
        {
            _appDBContext.Clientes.Update(cliente);
            _appDBContext.SaveChanges();
            return RedirectToAction(nameof(Stakeholders_Cliente));
        }

        [HttpPost]
        public IActionResult GoToMant(ClienteTrabajadorVM cliente_trabajador)
        {
            /*using var workbook = new XLWorkbook();
            var worksheet = workbook.AddWorksheet("Sample Sheet");
            worksheet.Cell("A1").Value = "Hello World 2!";
            worksheet.Cell("A2").FormulaA1 = "MID(A1, 7, 5)";
            workbook.Save();*/
            ViewBag.cliente_trabajador = cliente_trabajador;


            return View("Mantenimiento");
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
