namespace EMSI_Corporation.ViewModels
{
    public class MantenimientoPOSTVM
    {
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public int IdExtintor { get; set; }
        //mant

        //reporte servicio
        public IFormFile FirmaCliente { get; set; }
        public string Observaciones { get; set; }
        public IFormFile ImgEvidencia { get; set; }
        //Comprobante servicio
        public decimal PrecioUnitario { get; set; }

    }
}
