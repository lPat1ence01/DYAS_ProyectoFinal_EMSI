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
            if(ext == null || ext.Venta_ID == null)
            {
                var Mantenimientos = _appDBContext.Mantenimientos.Where(m => m.Extintor_ID == id).ToList();

                Index(id);
                return View("Index");
            }
            else
            {
                Empleado em = ext.Venta.Empleado;
                Cliente cli = ext.Venta.Cliente;

                ClienteTrabajadorVM ct = new ClienteTrabajadorVM
                {
                    cliente = cli,
                    empleado = em
                };

                return View("Registrar", ct);
            }

        }

    }
}
