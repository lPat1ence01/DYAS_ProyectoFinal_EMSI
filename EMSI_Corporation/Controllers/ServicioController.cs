using EMSI_Corporation.Data;
using EMSI_Corporation.Models;
using EMSI_Corporation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMSI_Corporation.Controllers
{
    public class ServicioController : Controller
    {
        private readonly AppDBContext _appDBContext;
        //prueba
        public ServicioController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SeleccionClienteTrabajador()
        {
            ViewBag.Clientes = _appDBContext.Clientes.ToList();
            ViewBag.Trabajadores = _appDBContext.empleados.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult SeleccionClienteTrabajador(ClienteTrabajadorVM ctVM)
        {
            //ViewBag.extintor = ext;//se supone que ya no
            ViewBag.mantenimientos = new List<Mantenimiento>();
            ViewBag.cliente_trabajador = ctVM;
            if (ctVM.TipoServicio == "Mantenimiento") {
                return RedirectToAction("Registrar","Mantenimiento");
            }
            else
            {
                return View();

            }
        }
    }
}
