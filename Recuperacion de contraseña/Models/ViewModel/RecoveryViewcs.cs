using System.ComponentModel.DataAnnotations;

namespace Recuperacion_de_contraseña.Models.ViewModel
{
    public class RecoveryViewcs
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
