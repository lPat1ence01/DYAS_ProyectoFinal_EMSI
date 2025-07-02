using System.ComponentModel.DataAnnotations;

namespace EMSI_Corporation.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
