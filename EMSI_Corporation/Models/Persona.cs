namespace EMSI_Corporation.Models
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string DNI { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string NumCelular { get; set; }
        public string Direccion { get; set; }

        public Cliente Cliente { get; set; }
        public Trabajador Trabajador { get; set; }
    }
}
