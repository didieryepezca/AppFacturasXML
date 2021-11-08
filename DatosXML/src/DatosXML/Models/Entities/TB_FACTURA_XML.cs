using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DatosXML.Models
{
    public class TB_FACTURA_XML
    {     
        [Key]
        [Display(Name = "CODIGO")]
        public int CODIGO { get; set; }

        [Display(Name = "RUC")]
        public string RUC { get; set; }

        [Display(Name = "RAZON SOCIAL")]
        public string RAZON_SOCIAL { get; set; }

        [Display(Name = "RUC BCO")]
        public string RUC_BANCO { get; set; }

        [Display(Name = "RAZ.SOC BCO")]
        public string RAZSOCIAL_BANCO { get; set; }

        [Display(Name = "TOTAL")]
        public decimal TOTAL { get; set; }

        [Display(Name = "MES Comision")]
        public string MES_COMISION { get; set; }
            
        [Display(Name = "Factura N°")]
        public string FACTURA_NUMERO { get; set; }

        [Display(Name = "OBSERVACIONES")]
        public string OBSERVACIONES { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "FECHA DE INGRESO")]
        public DateTime FECHA_INGRESO { get; set; }
     
    }
}







