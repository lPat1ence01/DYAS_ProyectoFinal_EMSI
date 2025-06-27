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
        public IActionResult Index()
        {

            Cliente? cli = _appDBContext.Clientes.Find(1);
            if (cli == null) return null;
            Empleado? em = _appDBContext.empleados.Find(1);
            if (em == null) return null;
            ClienteTrabajadorVM cliente_trabajador = new ClienteTrabajadorVM
            {
                cliente = cli,
                trabajador = em//////////////////////////////////////////////////////////
            };

            var mantenimientos = _appDBContext.Mantenimientos.ToList();

            ViewBag.cliente_trabajador = cliente_trabajador;
            ViewBag.mantenimientos = mantenimientos;

            return View();
        }

        [HttpGet]
        public IActionResult Lista()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AgregarMantenimiento()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MantenimientoPOST(MantenimientoVM mant)
        {
            Mantenimiento mantenimiento = new Mantenimiento
            {
                Empleado_ID = mant.IdEmpleado,
                Extintor_ID = mant.IdExtintor,
                AperturaCorrecta = mant.AperturaCorrecta,
                BarometroCorrecto = mant.BarometroCorrecto,
                BoquillaCorrecta = mant.BoquillaCorrecta,
                EstadoIndicador = mant.EstadoIndicador,
                EstadoPrecinto = mant.EstadoPrecinto,
                ExteriorCorrecto = mant.ExteriorCorrecto,
                FechaMantenimiento = mant.FechaMantenimiento,
                InstruccionesVisibles = mant.InstruccionesVisibles,
                LugarAdecuado = mant.LugarAdecuado,
                MangueraCorrecta = mant.MangueraCorrecta,
                PesoCorrecto = mant.PesoCorrecto,
                PresionCorrecta = mant.PresionCorrecta,
                Señalización = mant.Señalización,
                Usado = mant.Usado,
                Visible = mant.Visible,

            };
            _appDBContext.Mantenimientos.Add(mantenimiento);
            _appDBContext.SaveChanges();
            return View("Index");
        }

            [HttpPost]
        public IActionResult ServicioMantenimientoPOST(MantenimientoPOSTVM mant)
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


            ViewBag.cliente_trabajador = cliente_trabajador;
            _appDBContext.Mantenimientos.Add(mantenimiento);
            _appDBContext.SaveChanges();

            return View("Lista");


        }

        /*
         * 
            Mantenimiento mantenimiento = new Mantenimiento
            {
                Empleado_ID = mant.IdEmpleado,
                AperturaCorrecta = mant.AperturaCorrecta,
                BarometroCorrecto = mant.BarometroCorrecto,
                BoquillaCorrecta = mant.BoquillaCorrecta,
                EstadoIndicador = mant.EstadoIndicador,
                EstadoPrecinto = mant.EstadoPrecinto,
                ExteriorCorrecto = mant.ExteriorCorrecto,
                FechaMantenimiento = mant.FechaMantenimiento,
                Extintor_ID = mant.IdExtintor,
                InstruccionesVisibles = mant.InstruccionesVisibles,
                LugarAdecuado = mant.LugarAdecuado,
                MangueraCorrecta = mant.MangueraCorrecta,
                PesoCorrecto = mant.PesoCorrecto,
                PresionCorrecta = mant.PresionCorrecta,
                Señalización = mant.Señalización,
                Usado = mant.Usado,
                Visible = mant.Visible,

            };
         * */
    }
}
