using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DatosXML.Models.Entities
{
    public class TB_DISTRIBUCION_RECHAZOS
    {
        [Key]
        [Display(Name = "id")]
        public int id { get; set; }

        [Display(Name = "Motivo")]
        public string motivo { get; set; }

        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }

        [Display(Name = "% Rechazados")]
        public decimal porcentaje { get; set; }

        [Display(Name = "Mes")]
        public int mes { get; set; }

        [Display(Name = "Anio")]
        public int anio { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "FechaInicio")]
        public DateTime fecha_ini { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "FechaFin")]
        public DateTime fecha_fin { get; set; }
    }
}
