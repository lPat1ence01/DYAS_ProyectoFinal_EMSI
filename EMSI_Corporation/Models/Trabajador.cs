using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSI_Corporation.Models
{
    public class Trabajador
    {
        public int IdTrabajador {  get; set; }
        public string Cargo { get; set; }
        public decimal Sueldo { get; set; }
        public int PersonaID { get; set; }

        public Persona Persona { get; set; }
        public Usuario Usuario { get; set; }
    }
}
