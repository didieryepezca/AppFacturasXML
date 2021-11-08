using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatosXML.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "El {0} debe ser por lo menos {2} y maximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("Password", ErrorMessage = "No coincide el password con el anterior ingresdo.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Codigo { get; set; }
        [Required]
        [Display(Name = "Nombres")]
        public string nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }

    }
}
