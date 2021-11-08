using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DatosXML.Models.Entities
{
    public class TB_CONTROL_RECEPCION
    {
        [Key]
        [Display(Name = "id")]
        public int id { get; set; }

        [Display(Name = "Estatus")]
        public string estatus { get; set; }

        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }

        [Display(Name = "% Rechazados")]
        public decimal porcentaje_rechazados { get; set; }

        [Display(Name = "Mes Año")]
        public string mes_anio { get; set; }

        [Display(Name = "MesOrder")]
        public int mes_orden { get; set; }

        [Display(Name = "AnioOrder")]
        public int anio_orden { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "FechaInicio")]
        public DateTime fecha_ini { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "FechaFin")]
        public DateTime fecha_fin { get; set; }
    }
}
