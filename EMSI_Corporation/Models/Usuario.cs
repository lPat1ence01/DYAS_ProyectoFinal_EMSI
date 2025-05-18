namespace EMSI_Corporation.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Password  { get; set; }
        public string Rol {  get; set; }

        public int TrabajadorID { get; set; }

        public Trabajador Trabajador { get; set; }
    }
}
