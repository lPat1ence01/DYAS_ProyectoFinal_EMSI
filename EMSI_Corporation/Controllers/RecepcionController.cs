using EMSI_Corporation.Data;
using EMSI_Corporation.Models;
using EMSI_Corporation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMSI_Corporation.Controllers
{
    public class RecepcionController : Controller
    {
        private readonly AppDBContext _appDBContext;

        public RecepcionController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }


        //Ventana Principal
        [HttpGet]
        public IActionResult Index()
        {
            var proovedores = _appDBContext.Provedor.ToList();
            ViewBag.Proovedores = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(proovedores, "IdProovedor", "RazonSocial");
            return View();
        }

        //Realizar recepción
        [HttpGet]
        public IActionResult CrearRecepcion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearRecepcion(DateTime Fecha, int ProovedorId)
        {
            var nuevaRecepcion = new Recepcion
            {
                Fecha = Fecha,
                ProovedorId = ProovedorId
            };

            _appDBContext.Recepcion.Add(nuevaRecepcion);
            await _appDBContext.SaveChangesAsync();

            // Redirigir a la vista donde se registran los extintores, pasando el ID de la nueva recepción
            return RedirectToAction("RegistrarExtintores", new { recepcionId = nuevaRecepcion.IdRecepcion });
        }

        //Registrar Extintor
        [HttpGet]
        public IActionResult RegistrarExtintores(int recepcionId)
        {
            ViewBag.RecepcionId = recepcionId;

            var extintores = _appDBContext.Extintores
                .Where(e => e.RecepcionId == recepcionId)
                .ToList();

            ViewBag.Extintores = extintores;

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> GuardarExtintor(Extintor extintor)
        {
            _appDBContext.Extintores.Add(extintor);
            await _appDBContext.SaveChangesAsync();

            // Regresar a la misma vista para registrar más extintores si se desea
            return RedirectToAction("RegistrarExtintores", new { recepcionId = extintor.RecepcionId });
        }


    }
}
