namespace EMSI_Corporation.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string TipoCliente { get; set; }
        public string RUC { get; set; }
        public int PersonaID { get; set; }

        public Persona persona { get; set; }

    }
}
