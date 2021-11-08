using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DatosXML.Models
{
    public class TB_FACTURA_NO_LEIDA
    {
        [Key]
        [Display(Name = "CODIGO")]
        public int CODIGO { get; set; }

        [Display(Name = "NOMBRE DEL ARCHIVO")]
        public string NOMBRE_ARCHIVO { get; set; }

        [Display(Name = "MENSAJE DE ERROR ENCONTRADO")]
        public string MENSAJE_ERROR { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "FECHA DE INGRESO")]
        public DateTime FECHA_INGRESO { get; set; }
    }
}
