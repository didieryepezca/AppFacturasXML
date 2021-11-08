using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DatosXML.Models.Entities
{
    public class TB_CONTROL_RECEPCION_ANIO
    {
        [Key]
        [Display(Name = "id")]
        public int id { get; set; }        

        [Display(Name = "Monto Pendiente")]
        public decimal monto_pendiente { get; set; }

        [Display(Name = "Monto % Pendiente")]
        public decimal monto_porc_pendiente { get; set; }

        [Display(Name = "Monto Pagado")]
        public decimal monto_pagado { get; set; }

        [Display(Name = "Monto %Pagado")]
        public decimal monto_porc_pagado { get; set; }

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
