using EMSI_Corporation.Data;
using EMSI_Corporation.Views.Inventario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;

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
        }

        [HttpGet]
        public IActionResult VerPDF()
        {
            var pdf = new InventarioPDF(_appDBContext);
            var documento = pdf.CreatePDF();

            var stream = new MemoryStream();
            documento.GeneratePdf(stream);
            stream.Position = 0;

            // Visualizar en navegador
            return File(stream, "application/pdf");
        }

        [HttpGet]
        public IActionResult DescargarPDF()
        {
            var pdf = new InventarioPDF(_appDBContext);
            var documento = pdf.CreatePDF();

            var stream = new MemoryStream();
            documento.GeneratePdf(stream);
            stream.Position = 0;

            // Forzar descarga
            return File(stream, "application/pdf", "InventarioExtintores.pdf");
        }
    }
}
