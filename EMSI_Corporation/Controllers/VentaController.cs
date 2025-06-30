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
            for (int i = 0; i < ventas.Count; i++)
            {
                ventas[i].Cliente = _appDBContext.Clientes.Find(ventas[i].Cliente_ID);
                ventas[i].Empleado = _appDBContext.empleados.Find(ventas[i].Empleado_ID);
            }

            return View(ventas);
        }

        public IActionResult VentasPorId(int id)
        {
            var Extintor = _appDBContext.Extintores.Find(id);
            Venta vent = Extintor.Venta;
            ViewBag.ExtintorId = id;
            return View(vent);
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
