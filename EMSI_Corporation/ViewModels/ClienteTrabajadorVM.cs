using EMSI_Corporation.Models;

namespace EMSI_Corporation.ViewModels
{
    public class ClienteTrabajadorVM
    {
        public Cliente cliente { get; set; }
        public Empleado empleado { get; set; }
        public string TipoServicio { get; set; }
    }
}
