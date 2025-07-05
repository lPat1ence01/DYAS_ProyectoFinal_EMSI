using System.ComponentModel.DataAnnotations;

namespace EMSI_Corporation.ViewModels
{
    public class LoginVM
    {
        [StringLength(100, ErrorMessage = "El usuario no puede tener más de 100 caracteres"), Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria"), StringLength(50, ErrorMessage = "La contraseña no puede tener más de 50 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
