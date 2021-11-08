using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DatosXML.Models.Entities
{
    public class TB_PRINCIPALES_INDICADORES
    {
        [Key]
        [Display(Name = "id")]
        public int id { get; set; }

        [Display(Name = "Monto a Pagar")]
        public decimal monto_pagar { get; set; }

        [Display(Name = "Total Pagado")]
        public decimal total_pagado { get; set; }

        [Display(Name = "Documentos Esperados")]
        public int documentos_esperados { get; set; }

        [Display(Name = "Documentos Atendidos")]
        public int documentos_atendidos { get; set; }

        [Display(Name = "Media Mensual Docs. Atendidos")]
        public decimal media_mensual_docs_aten { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "FechaInicio")]
        public DateTime fecha_ini { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "FechaFin")]
        public DateTime fecha_fin { get; set; }
    }
}
