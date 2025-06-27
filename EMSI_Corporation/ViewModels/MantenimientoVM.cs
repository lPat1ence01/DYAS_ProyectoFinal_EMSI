namespace EMSI_Corporation.ViewModels
{
    public class MantenimientoVM
    {
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public int IdExtintor { get; set; }
        public int id_extintor {  get; set; }
        public DateTime FechaMantenimiento { get; set; }
        public bool LugarAdecuado { get; set; }
        public bool Señalización { get; set; }
        public bool Visible { get; set; }
        public bool Usado { get; set; }
        public bool EstadoPrecinto { get; set; }
        public bool EstadoIndicador { get; set; }
        public bool PresionCorrecta { get; set; }
        public bool ExteriorCorrecto { get; set; }
        public bool PesoCorrecto { get; set; }
        public bool MangueraCorrecta { get; set; }
        public bool BoquillaCorrecta { get; set; }
        public bool InstruccionesVisibles { get; set; }
        public bool AperturaCorrecta { get; set; }
        public bool BarometroCorrecto { get; set; }
    }
}
