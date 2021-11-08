using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DatosXML.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Display(Name = "Nombres")]
        public string nombres { get; set; }
        public string Apellidos { get; set; }
    }
}
