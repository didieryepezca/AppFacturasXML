using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DatosXML.Models.Entities
{
    public class TB_CONTROL_PROCESADAS_DIA
    {
        [Key]
        [Display(Name = "id")]
        public int id { get; set; }

        [Display(Name = "Estatus")]
        public string estatus { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Dia")]
        public DateTime dia_fecha { get; set; }

        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "FechaInicio")]
        public DateTime fecha_ini { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "FechaFin")]
        public DateTime fecha_fin { get; set; }

    }
}
