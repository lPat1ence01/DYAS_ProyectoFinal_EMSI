using EMSI_Corporation.Data;
using EMSI_Corporation.Models;
using EMSI_Corporation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EMSI_Corporation.Controllers
{
    public class MantenimientoController : Controller
    {
        private readonly AppDBContext _appDBContext;

        public MantenimientoController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public IActionResult Index(int id)
        {
            var Extintor = _appDBContext.Extintores.Find(id);
            var Mantenimientos = _appDBContext.Mantenimientos.Where(m => m.Extintor_ID == id).ToList();
            ViewBag.ExtintorId = id;
            return View(Mantenimientos);
        }
        public IActionResult Registrar(int id)
        {
            Extintor? ext = _appDBContext.Extintores.Find(id);
            if(false)
            {
                var Mantenimientos = _appDBContext.Mantenimientos.Where(m => m.Extintor_ID == id).ToList();

                Index(id);
                return View("Index");
            }
            else
            {
                Venta v = _appDBContext.Ventas.Find(ext.Venta_ID);
                ext.Venta = v;
                /*d
                Empleado em = ext.Venta.Empleado;
                Cliente cli = ext.Venta.Cliente;
                */
                ClienteTrabajadorVM ct = new ClienteTrabajadorVM
                {
                    cliente = v.Cliente,
                    empleado = v.Empleado
                };

                return View("Registrar", ct);
            }

        }

    }
}
