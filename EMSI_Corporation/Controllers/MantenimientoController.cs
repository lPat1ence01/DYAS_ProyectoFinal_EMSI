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
            for (int i = 0; i < Mantenimientos.Count; i++)
            {
                Mantenimientos[i].Empleado = _appDBContext.empleados.Find(Mantenimientos[i].Empleado_ID);
                Mantenimientos[i].Extintor = _appDBContext.Extintores.Find(Mantenimientos[i].Extintor_ID);
            }
            ViewBag.ExtintorId = id;
            return View(Mantenimientos);
        }
        public IActionResult Registrar(int id)
        {
            Extintor? ext = _appDBContext.Extintores.Find(id);
            var Mantenimientos = _appDBContext.Mantenimientos.Where(m => m.Extintor_ID == id).ToList();
            if (false)
            {


                Index(id);
                return View("Index");
            }
            else
            {
                Venta v = _appDBContext.Ventas.Find(ext.Venta_ID);
                ext.Venta = v;

                Empleado em = _appDBContext.empleados.Find(v.Empleado_ID);
                Cliente cli = _appDBContext.Clientes.Find(v.Cliente_ID);

                ClienteTrabajadorVM ct = new ClienteTrabajadorVM
                {
                    cliente = cli,
                    empleado = em
                };

                ViewBag.extintor = ext;
                ViewBag.mantenimientos = Mantenimientos;
                ViewBag.cliente_trabajador = ct;

                return View("Registrar");
            }

        }

        [HttpPost]
        public IActionResult Registrar(ServicioMantenimientoVM mantVM)
        {
            Cliente cli = _appDBContext.Clientes.Find(mantVM.Cliente_ID);

            ComprobanteServicio cs = new ComprobanteServicio
            {
                Cantidad = mantVM.Cantidad,
                PrecioUnitario = mantVM.PrecioUnitario,
                SubTotal = mantVM.SubTotal,
                TipoServicio = "Mantenimiento",
            };
            _appDBContext.ComprobantesServicio.Add(cs);
            _appDBContext.SaveChanges();
            ReporteServicio rs = new ReporteServicio
            {
                Cliente = cli,
                Cliente_ID = cli.IdCliente,
                ComprobanteServicio = cs,
                Comprobante_ID = cs.IdComprobante
            };
            _appDBContext.ReportesServicio.Add(rs);
            _appDBContext.SaveChanges();

            return View("Index");
        }

        [HttpGet]
        public IActionResult RegistrarEstadoExtintor(int id)
        {
            Extintor ext = _appDBContext.Extintores.Find(id);
            Venta vent = _appDBContext.Ventas.Find(ext.Venta_ID);
            Empleado em = _appDBContext.empleados.Find(vent.Empleado_ID);
            ViewBag.extintor = ext;
            ViewBag.venta = vent;
            ViewBag.empleado = em;
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarEstadoExtintor(MantenimientoVM mantVM)
        {
            Empleado em = _appDBContext.empleados.Find(mantVM.Empleado_ID);
            Extintor ex = _appDBContext.Extintores.Find(mantVM.Extintor_ID);
            Mantenimiento mant = new Mantenimiento
            {
                AperturaCorrecta = mantVM.AperturaCorrecta,
                BarometroCorrecto = mantVM.BarometroCorrecto,
                BoquillaCorrecta = mantVM.BoquillaCorrecta,
                EstadoIndicador = mantVM.EstadoIndicador,
                EstadoPrecinto = mantVM.EstadoPrecinto,
                ExteriorCorrecto = mantVM.ExteriorCorrecto,
                FechaMantenimiento = DateTime.Now,
                InstruccionesVisibles = mantVM.InstruccionesVisibles,
                LugarAdecuado = mantVM.LugarAdecuado,
                MangueraCorrecta = mantVM.MangueraCorrecta,
                PesoCorrecto = mantVM.PesoCorrecto,
                PresionCorrecta = mantVM.PresionCorrecta,
                Señalización = mantVM.Señalización,
                Usado = mantVM.Usado,
                Visible = mantVM.Visible,
                Empleado_ID = mantVM.Empleado_ID,
                Extintor_ID = mantVM.Extintor_ID,
                Empleado = em,
                Extintor = ex,
            };
            _appDBContext.Mantenimientos.Add(mant);
            _appDBContext.SaveChanges();

            ViewBag.Extintor = ex;
            Venta ven = _appDBContext.Ventas.Find(ex.Venta_ID);
            ClienteTrabajadorVM ct = new ClienteTrabajadorVM
            {
                cliente = _appDBContext.Clientes.Find(ven.Cliente_ID),
                empleado = em
            };
            ViewBag.mantenimientos = _appDBContext.Mantenimientos.Where(m => m.Extintor_ID == ex.IdExtintor).ToList();
            ViewBag.cliente_trabajador = ct;
            return View("Registrar");
        }
    }
}
