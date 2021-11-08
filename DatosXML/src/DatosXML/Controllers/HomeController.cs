using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Xml;
using DatosXML.Models;
using DatosXML.Models.Entities;
using DatosXML.Data.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;


namespace DatosXML.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;   
        private IHostingEnvironment hostingEnv;
       
        public HomeController(IHostingEnvironment hostingEnv,  UserManager<ApplicationUser> userManager)        
        {         
            this.userManager = userManager;          
            this.hostingEnv = hostingEnv;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //TB_FACTURA_DA da1 = new TB_FACTURA_DA();
            //var model = da1.GetXML(Convert.ToDateTime("0001-01-01"));
            return View();           
        }

        public int FunCargarCSV(string NombreCSV)
        {
            var mensajeError = "";
            var da = new IndicadoresDA();

            try
            {                
                var modelcount = da.CargarConsolidadoCSV(NombreCSV);

                return modelcount;
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
                return 0;
            }
        }


        public int FunCalcularIndicadores(DateTime vFechaIni, DateTime vFechaFin)
        {
            var mensajeError = "";
            var da = new IndicadoresDA();

            try
            {
                var modelcount = da.CalcularIndicadores(vFechaIni, vFechaFin);

                return modelcount;
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
                return 0;
            }
        }


        public List<TB_PRINCIPALES_INDICADORES> FunGetMainIndicadores(DateTime vFechaIni, DateTime vFechaFin)
        {            
            var da = new IndicadoresDA();            

            var model = da.GetIndicadoresPrincipales(vFechaIni, vFechaFin).ToList();

            return model;
        }


        public List<TB_CONTROL_RECEPCION> FunGetCtrolRecepcion(DateTime vFechaIni, DateTime vFechaFin)
        {
            var da = new IndicadoresDA();

            var model = da.GetControlRecepcion(vFechaIni, vFechaFin).ToList();

            return model;
        }

        public List<TB_CONTROL_RECEPCION_DIA> FunGetCtrolRecepcionDia(DateTime vFechaIni, DateTime vFechaFin)
        {
            var da = new IndicadoresDA();

            var model = da.GetControlRecepcionDia(vFechaIni, vFechaFin).ToList();

            return model;
        }


        public List<TB_DISTRIBUCION_RECHAZOS> FunGetDistRechazos(DateTime vFechaIni, DateTime vFechaFin)
        {
            var da = new IndicadoresDA();

            var model = da.GetDistribucionRechazos(vFechaIni, vFechaFin).ToList();

            return model;
        }


        public List<TB_CONTROL_RECEPCION_ANIO> FunGetMontosAnio(DateTime vFechaIni, DateTime vFechaFin)
        {
            var da = new IndicadoresDA();

            var model = da.GetMontosAnio(vFechaIni, vFechaFin).ToList();

            return model;
        }

        public List<TB_CONTROL_PROCESADAS_DIA> FunGetProcesadasDia(DateTime vFechaIni, DateTime vFechaFin)
        {
            var da = new IndicadoresDA();

            var model = da.GetProcesadasDia(vFechaIni, vFechaFin).ToList();

            return model;
        }


        

        [HttpGet]
        public IActionResult HistorialCargas()
        {
            DateTime fechaHoy = DateTime.Now;
            ViewBag.fecha_hoy = fechaHoy.ToString("yyyy-MM-dd");

            IndicadoresDA da = new IndicadoresDA();

            var model = da.GetAllCargasCSV(fechaHoy);
            
            return View(model);
        }


        [HttpPost]
        public IActionResult HistorialCargas(DateTime fecha_ingreso)
        {
            DateTime fechaHoy = DateTime.Now;
            ViewBag.fecha_hoy = fechaHoy.ToString("yyyy-MM-dd");

            if (fecha_ingreso.Date == Convert.ToDateTime("01/01/0001").Date)
            {
                fecha_ingreso = DateTime.Now;
            }

            IndicadoresDA da = new IndicadoresDA();

            var model = da.GetAllCargasCSV(fecha_ingreso);

            return View(model);
        }





        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }       
    }
}

