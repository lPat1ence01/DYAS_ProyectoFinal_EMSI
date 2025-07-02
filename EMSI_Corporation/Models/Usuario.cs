using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSI_Corporation.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }

        public int? IdEmpleado { get; set; }

        /* Relación con Empleado */
        [ForeignKey("IdEmpleado")]
        public Empleado empleado { get; set; }

        public ICollection<User_Rol> UserRoles { get; set; }

        /* Relacion con Recepcion */
        public ICollection<Recepcion> Recepciones { get; set; }
    }
}
