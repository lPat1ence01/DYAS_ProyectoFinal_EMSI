using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSI_Corporation.Models
{
    public class Recarga
    {
        [Key]
        public int IdRecarga { get; set; }
        public DateTime FechaRecarga { get; set; }
        public string MaterialUsado { get; set; }
        public decimal CantidadKG { get; set; }
        public decimal PresionPostRecarga { get; set; }

        [ForeignKey("Extintor")]
        public int Extintor_ID { get; set; }
        public Extintor Extintor { get; set; }

        [ForeignKey("Empleado")]
        public int Empleado_ID { get; set; }
        public Empleado Empleado { get; set; }
    }
}
