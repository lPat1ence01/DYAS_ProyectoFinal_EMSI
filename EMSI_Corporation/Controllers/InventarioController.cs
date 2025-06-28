using EMSI_Corporation.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMSI_Corporation.Controllers
{
    public class InventarioController : Controller
    {

        private readonly AppDBContext _appDBContext;

        public InventarioController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Inventario()
        {
            var extintores = await _appDBContext.Extintores.ToListAsync();
            return View(extintores);
            //hola
        }
        [HttpGet]
        public async Task<IActionResult> AgregarExtintor()
        {
            return View();
            //hola
        }
    }
}
