using EMSI_Corporation.Data;
using EMSI_Corporation.Migrations;
using EMSI_Corporation.Models;
using EMSI_Corporation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EMSI_Corporation.Controllers
{
    public class VentaController : Controller
    {
        private readonly AppDBContext _appDBContext;

        public VentaController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public IActionResult Index()
        {
            var ventas = _appDBContext.Ventas.ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Registrar(int idExtintor)
        {
            ViewBag.clientes = _appDBContext.Clientes.ToList();
            ViewBag.empleados = _appDBContext.empleados.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(VentaVM ventaVM)
        {
            Cliente? cli = _appDBContext.Clientes.Find(ventaVM.Cliente_ID);
            Empleado? empleado = _appDBContext.empleados.Find(ventaVM.Empleado_ID);

            Venta venta = new Venta
            {
                Cliente = cli,
                Cliente_ID = ventaVM.Cliente_ID,
                Empleado = empleado,
                Empleado_ID = ventaVM.Empleado_ID,
                FechaVenta = ventaVM.FechaVenta,
                MetodoPago = ventaVM.MetodoPago,
                Total = ventaVM.Total
            };
            _appDBContext.Ventas.Add(venta);
            _appDBContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
