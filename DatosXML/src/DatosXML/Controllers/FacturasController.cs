using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Xml;
using DatosXML.Models;
using DatosXML.Data;
using System.Linq;

namespace DatosXML.Controllers
{
    public class FacturasController : Controller
    {
        public IActionResult Index()
        {
            DateTime fechaHoy = DateTime.Now;
            ViewBag.fecha_hoy = fechaHoy.ToString("yyyy-MM-dd");

            TB_FACTURA_DA da1 = new TB_FACTURA_DA();

            var model = da1.GetXML(fechaHoy);
            return View(model);

        }

        [HttpPost]
        public IActionResult Index(DateTime fecha_ingreso, string accion = "")
        {
            DateTime fechaHoy = DateTime.Now;
            ViewBag.fecha_hoy = fechaHoy.ToString("yyyy-MM-dd");

            TB_FACTURA_DA da = new TB_FACTURA_DA();

            if (accion == "B")
            {
                if (fecha_ingreso.Date == Convert.ToDateTime("01/01/0001").Date)
                {
                    fecha_ingreso = DateTime.Now;
                }

                var model = da.GetFacturasFechaIngreso(fecha_ingreso);

                return View(model);
            }
            else
            {
                string folderName = @"\\172.17.1.51\webpymes";

                var uploads = System.IO.Path.Combine(folderName, "FacturasXML");

                DirectoryInfo di = new DirectoryInfo(uploads);
                Console.WriteLine("No search pattern returns:");                            

                var eliminar = da.BorrarDiario(DateTime.Now);
                var eliminarNonRead = da.BorrarDiarioNoLeidas(DateTime.Now);

                foreach (var fi in di.GetFiles("*.xml*"))
                {
                    int counterGlobal = 0;
                                                     

                    //------------------------------------- Instancia de XmlDocument 
                    XmlDocument xDoc1 = new XmlDocument();

                    try
                    {
                        xDoc1.Load(fi.FullName);

                        XmlNodeList primerNivelDetalleConteo = xDoc1.GetElementsByTagName("cac:Item");
                        counterGlobal = primerNivelDetalleConteo.Count;

                        //XmlNodeList segundoNivelDetalleConteo = ((XmlElement)primerNivelDetalleConteo[i]).GetElementsByTagName("cbc:Description");
                    }
                    catch (Exception e)
                    {
                        var ex = e.Message;
                    }

                    for (int j = 0; j <= counterGlobal; j++)
                    {
                        TB_FACTURA_XML factura = new TB_FACTURA_XML();
                        factura.FECHA_INGRESO = Convert.ToDateTime(System.DateTime.Now);

                        try
                        {
                            //------------------------------------- Carga XmlDocument 
                            xDoc1.Load(fi.FullName);
                            //------------------------------------- Carga XmlDocument 

                            XmlNodeList lista1;
                            XmlNodeList personas1;

                            if (xDoc1.GetElementsByTagName("cac:PartyIdentification").Count > 0)
                            {
                                personas1 = xDoc1.GetElementsByTagName("cac:PartyIdentification");
                                lista1 = ((XmlElement)personas1[0]).GetElementsByTagName("cbc:ID");

                                foreach (XmlElement nodo in lista1)
                                {
                                    if (nodo.InnerText.Length == 11)
                                    {
                                        var ruc = nodo.InnerText;
                                        factura.RUC = ruc;
                                    }
                                }
                            }
                            else if (xDoc1.GetElementsByTagName("cbc:CustomerAssignedAccountID").Count > 0)
                            {

                                personas1 = xDoc1.GetElementsByTagName("cac:AccountingSupplierParty");
                                lista1 = ((XmlElement)personas1[0]).GetElementsByTagName("cbc:CustomerAssignedAccountID");

                                foreach (XmlElement nodo in lista1)
                                {
                                    if (nodo.InnerText.Length == 11)
                                    {
                                        var ruc = nodo.InnerText;
                                        factura.RUC = ruc;

                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            TB_FACTURA_NO_LEIDA factura_non_read = new TB_FACTURA_NO_LEIDA();

                            factura_non_read.NOMBRE_ARCHIVO = fi.Name;
                            factura_non_read.MENSAJE_ERROR = e.Message;
                            factura_non_read.FECHA_INGRESO = DateTime.Now;

                            da.InsertFacturaNoLeida(factura_non_read);

                            var ex = e.Message;
                        }

                        try
                        {

                            XmlNodeList primerNivel = xDoc1.GetElementsByTagName("cac:PartyName");
                            XmlNodeList segundoNivel = ((XmlElement)primerNivel[0]).GetElementsByTagName("cbc:Name");


                            foreach (XmlElement nodo in segundoNivel)
                            {

                                var razSocial = nodo.InnerText;
                                factura.RAZON_SOCIAL = razSocial;

                            }
                        }
                        catch (Exception e)
                        {
                            var ex = e.Message;
                        }

                        // RUC BANCO--------------------
                        try
                        {                           

                            XmlNodeList listaRuc;
                            XmlNodeList listaRucNv2;

                            if (xDoc1.GetElementsByTagName("cac:PartyIdentification").Count > 0)
                            {
                                listaRucNv2 = xDoc1.GetElementsByTagName("cac:PartyIdentification");
                                listaRuc = ((XmlElement)listaRucNv2[1]).GetElementsByTagName("cbc:ID");

                                foreach (XmlElement nodo in listaRuc)
                                {
                                    string[] RUCCBBVA = { "20100130204" };

                                    foreach (string bbva in RUCCBBVA) {

                                        if (nodo.InnerText.Contains(bbva))
                                        {
                                            var rucBanco = nodo.InnerText;
                                            factura.RUC_BANCO = rucBanco;
                                        }

                                    }
                                    
                                }
                            }
                            if (xDoc1.GetElementsByTagName("cac:PartyIdentification").Count > 0)
                            {
                                listaRucNv2 = xDoc1.GetElementsByTagName("cac:PartyIdentification");
                                listaRuc = ((XmlElement)listaRucNv2[2]).GetElementsByTagName("cbc:ID");

                                foreach (XmlElement nodo in listaRuc)
                                {
                                    string[] RUCCBBVA = { "20100130204" };

                                    foreach (string bbva in RUCCBBVA)
                                    {

                                        if (nodo.InnerText.Contains(bbva))
                                        {
                                            var rucBanco = nodo.InnerText;
                                            factura.RUC_BANCO = rucBanco;
                                        }

                                    }

                                }
                            }

                            else if (xDoc1.GetElementsByTagName("cbc:CustomerAssignedAccountID").Count > 0)
                            {

                                listaRuc = xDoc1.GetElementsByTagName("cac:AccountingSupplierParty");
                                listaRucNv2 = ((XmlElement)listaRuc[1]).GetElementsByTagName("cbc:CustomerAssignedAccountID");

                                foreach (XmlElement nodo in listaRucNv2)
                                {
                                    string[] RUCCBBVA = { "20100130204" };

                                    foreach (string bbva in RUCCBBVA)
                                    {

                                        if (nodo.InnerText.Contains(bbva))
                                        {
                                            var rucBanco = nodo.InnerText;
                                            factura.RUC_BANCO = rucBanco;
                                        }

                                    }
                                   
                                }
                            }
                        }
                        catch (Exception e)
                        {                           

                            var ex = e.Message;
                        }
                        //--------------------------------------------------------------------------------------

                        // RAZON SOCIAL BANCO
                        try
                        {

                            XmlNodeList frstNameRazSocBanco = xDoc1.GetElementsByTagName("cac:PartyLegalEntity");
                            XmlNodeList scdNameRazSocBanco = ((XmlElement)frstNameRazSocBanco[1]).GetElementsByTagName("cbc:RegistrationName");


                            foreach (XmlElement nodo in scdNameRazSocBanco)
                            {

                                var razSocialBco = nodo.InnerText;
                                factura.RAZSOCIAL_BANCO = razSocialBco;

                            }
                        }
                        catch (Exception e)
                        {
                            var ex = e.Message;
                        }

                        //--------------------------------------------------------------------------------------

                        if (counterGlobal > 1) {

                            // TOTAL GENERAL PARA FACTURA CON VARIOS MESES
                            try
                            {

                                XmlNodeList priNivelFactMeses = xDoc1.GetElementsByTagName("cac:Price");
                                XmlNodeList secNivelFactMeses = ((XmlElement)priNivelFactMeses[j]).GetElementsByTagName("cbc:PriceAmount");

                                foreach (XmlElement nodo in secNivelFactMeses)
                                {

                                    var impTotal = nodo.InnerText;
                                    factura.TOTAL = Convert.ToDecimal(impTotal);
                                }
                            }
                            catch (Exception e)
                            {
                                var ex = e.Message;
                            }


                        } else {
                        
                            // TOTAL GENERAL PARA FACTURA CON UN MES
                            try
                            {

                                XmlNodeList priNivel = xDoc1.GetElementsByTagName("cac:LegalMonetaryTotal");
                                XmlNodeList secNivel = ((XmlElement)priNivel[0]).GetElementsByTagName("cbc:PayableAmount");

                                foreach (XmlElement nodo in secNivel)
                                {

                                    var impTotal = nodo.InnerText;
                                    factura.TOTAL = Convert.ToDecimal(impTotal);
                                }
                            }
                            catch (Exception e)
                            {
                                var ex = e.Message;
                            }
                        }
                        
                        //MES COMISION
                        try
                        {
                            XmlNodeList prNivel = xDoc1.GetElementsByTagName("cac:Item");
                            XmlNodeList scNivel = ((XmlElement)prNivel[j]).GetElementsByTagName("cbc:Description");

                            foreach (XmlElement nodo in scNivel)
                            {
                                string[] mesesArray = {"ENERO","FEBRERO","MARZO","ABRIL",
                            "MAYO","JUNIO","JULIO","AGOSTO","SEPTIEMBRE","SETIEMBRE","OCTUBRE",
                            "NOVIEMBRE", "DICIEMBRE", "Enero","Febrero","Marzo","Abril",
                            "Mayo","Junio","Julio","Agosto","Septiembre","Setiembre","Octubre",
                            "Noviembre", "Diciembre"  };

                                string[] anosArray = { "2017", "2018", "2019", "2020", "2021" };

                                foreach (string m in mesesArray)
                                {
                                    foreach (string a in anosArray)
                                    {
                                        if (nodo.InnerText.Contains(m))
                                        {
                                            if (nodo.InnerText.Contains(a))
                                            {

                                                var mesDescripcion = nodo.InnerText;
                                                factura.MES_COMISION = m + " " + a;
                                            }
                                        }
                                    }
                                }
                            }


                        }
                        catch (Exception e)
                        {
                            var ex = e.Message;
                        }
                        try
                        {
                            XmlNodeList firstLevel;
                            XmlNodeList secLevel;

                            if (xDoc1.GetElementsByTagName("Invoice").Count > 0)
                            {
                                firstLevel = xDoc1.GetElementsByTagName("Invoice");
                                secLevel = ((XmlElement)firstLevel[0]).GetElementsByTagName("cbc:ID");

                                foreach (XmlElement nodo in secLevel)
                                {
                                    if (nodo.InnerText.Substring(0, 1) == "F" ||
                                       nodo.InnerText.Substring(0, 2) == "F0" ||
                                        nodo.InnerText.Substring(0, 1) == "E")
                                    {
                                        var nroComprobante = nodo.InnerText;

                                        factura.FACTURA_NUMERO = nroComprobante;

                                        if (nodo.InnerText.Length < 12)
                                        {
                                            var indexGuion = nodo.InnerText.IndexOf('-');

                                            int c = 12 - nodo.InnerText.Length;
                                            var cad = "";
                                            for (int i = 0; i < c; i++)
                                            {
                                                cad = cad + "0";

                                            }

                                            factura.FACTURA_NUMERO = nroComprobante.Insert(5, cad);
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            var ex = e.Message;
                        }


                        //OBSERVACIONES
                        try
                        {
                            XmlNodeList frstLvl = xDoc1.GetElementsByTagName("cac:Item");
                            XmlNodeList secLvl = ((XmlElement)frstLvl[j]).GetElementsByTagName("cbc:Description");

                            foreach (XmlElement nodo in secLvl)
                            {

                                var observs = nodo.InnerText;
                                factura.OBSERVACIONES = observs;
                            }

                        }
                        catch (Exception e)
                        {
                            var ex = e.Message;
                        }                        

                        if (factura.TOTAL.Equals(null) ||
                            factura.FACTURA_NUMERO == null ||
                            factura.OBSERVACIONES == null)
                        {
                            Console.WriteLine(":)");
                        }

                        else
                        {
                            var result = da.Insert(factura);
                        }
                    }

                    
                }

                var modelNonRead = da.GetFacturasNoLeidas(fechaHoy);
                ViewBag.NonRead = modelNonRead.Count();

                var model = da.GetXML(fechaHoy);
                return View(model);

            }
        }



        public IActionResult FacNonRead(DateTime fecha_ingreso)
        {
            DateTime fechaHoy = DateTime.Now;
            ViewBag.fecha_hoy = fechaHoy.ToString("yyyy-MM-dd");

            TB_FACTURA_DA da = new TB_FACTURA_DA();


            if (fecha_ingreso.Date == Convert.ToDateTime("01/01/0001").Date)
            {
                fecha_ingreso = DateTime.Now;
            }

            var model = da.GetFacturasNoLeidas(fecha_ingreso);

            return View(model);

        }

        [HttpPost]
        public IActionResult FacNonRead(DateTime fecha_ingreso, string accion = "")
        {
            DateTime fechaHoy = DateTime.Now;
            ViewBag.fecha_hoy = fechaHoy.ToString("yyyy-MM-dd");

            if (fecha_ingreso.Date == Convert.ToDateTime("01/01/0001").Date)
            {
                fecha_ingreso = DateTime.Now;
            }

            TB_FACTURA_DA da = new TB_FACTURA_DA();
            var model = da.GetFacturasNoLeidas(fecha_ingreso);

            return View(model);

        }




    }
}