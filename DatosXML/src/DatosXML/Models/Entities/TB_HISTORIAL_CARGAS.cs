using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DatosXML.Models.Entities
{
    public class TB_HISTORIAL_CARGAS
    {
        [Key]
        [Display(Name = "Codigo")]
        public int codigo { get; set; }

        [Display(Name = "Estado")]
        public string estado { get; set; }

        [Display(Name = "Archivo")]
        public string archivo { get; set; }

        [DisplayFormat(DataFormatString = "{0:ddd, d MMMM yyyy, hh:mm tt}")]
        [Display(Name = "Fecha de Carga")]
        public DateTime fecha { get; set; }
    }
}
