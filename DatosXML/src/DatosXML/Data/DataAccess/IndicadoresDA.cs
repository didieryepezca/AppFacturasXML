using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DatosXML.Models.Entities;


namespace DatosXML.Data.DataAccess
{
    public class IndicadoresDA
    {
        public int CargarConsolidadoCSV(string vNombArchivo)
        {
            var result = 0;
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    SqlParameter nombreArchivo = new SqlParameter();
                    nombreArchivo.ParameterName = "@archivo";
                    nombreArchivo.Value = vNombArchivo;
                    nombreArchivo.SqlDbType = SqlDbType.VarChar;
                    nombreArchivo.Direction = System.Data.ParameterDirection.Input;                   

                    result = db.Database.ExecuteSqlCommand("SUBIR_DATA_CONSOLIDADO @archivo ", nombreArchivo);                   
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            return result;
        }


        public int CalcularIndicadores(DateTime FechaIni, DateTime FechaFin)
        {
            var result = 0;
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    SqlParameter fechaInicio = new SqlParameter();
                    fechaInicio.ParameterName = "@fecha_ini";
                    fechaInicio.Value = FechaIni;
                    fechaInicio.SqlDbType = SqlDbType.Date;
                    fechaInicio.Direction = System.Data.ParameterDirection.Input;

                    SqlParameter fechaFin = new SqlParameter();
                    fechaFin.ParameterName = "@fecha_fin";
                    fechaFin.Value = FechaFin;
                    fechaFin.SqlDbType = SqlDbType.Date;
                    fechaFin.Direction = System.Data.ParameterDirection.Input;

                    result = db.Database.ExecuteSqlCommand("CALCULAR_INDICADORES_CONSOLIDADO @fecha_ini, @fecha_fin", fechaInicio, fechaFin);
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            return result;
        }

        public IEnumerable<TB_PRINCIPALES_INDICADORES> GetIndicadoresPrincipales(DateTime fecha_ini,DateTime fecha_fin)
        {
            var list = new List<TB_PRINCIPALES_INDICADORES>();

            using (var db = new ApplicationDbContext())
            {
                IQueryable<TB_PRINCIPALES_INDICADORES> query = db.TB_PRINCIPALES_INDICADORES;

                query = query.Where(item => item.fecha_ini.Date == fecha_ini.Date && item.fecha_fin.Date == fecha_fin.Date);                              

                list = query.ToList();
            }
            return list;
        }


        public IEnumerable<TB_CONTROL_RECEPCION> GetControlRecepcion(DateTime fecha_ini, DateTime fecha_fin)
        {
            var list = new List<TB_CONTROL_RECEPCION>();

            using (var db = new ApplicationDbContext())
            {
                IQueryable<TB_CONTROL_RECEPCION> query = db.TB_CONTROL_RECEPCION;

                query = query.Where(item => item.fecha_ini.Date == fecha_ini.Date && item.fecha_fin.Date == fecha_fin.Date);

                list = query.OrderBy(c => c.id).ToList();
            }
            return list;
        }


        public IEnumerable<TB_CONTROL_RECEPCION_DIA> GetControlRecepcionDia(DateTime fecha_ini, DateTime fecha_fin)
        {
            var list = new List<TB_CONTROL_RECEPCION_DIA>();

            using (var db = new ApplicationDbContext())
            {
                IQueryable<TB_CONTROL_RECEPCION_DIA> query = db.TB_CONTROL_RECEPCION_DIA;

                query = query.Where(item => item.fecha_ini.Date == fecha_ini.Date && item.fecha_fin.Date == fecha_fin.Date);

                list = query.OrderBy(c => c.id).ToList();
            }
            return list;
        }


        public IEnumerable<TB_DISTRIBUCION_RECHAZOS> GetDistribucionRechazos(DateTime fecha_ini, DateTime fecha_fin)
        {
            var list = new List<TB_DISTRIBUCION_RECHAZOS>();

            using (var db = new ApplicationDbContext())
            {
                IQueryable<TB_DISTRIBUCION_RECHAZOS> query = db.TB_DISTRIBUCION_RECHAZOS;

                query = query.Where(item => item.fecha_ini.Date == fecha_ini.Date && item.fecha_fin.Date == fecha_fin.Date);

                list = query.OrderBy(c => c.id).ToList();
            }
            return list;
        }


        public IEnumerable<TB_CONTROL_RECEPCION_ANIO> GetMontosAnio(DateTime fecha_ini, DateTime fecha_fin)
        {
            var list = new List<TB_CONTROL_RECEPCION_ANIO>();

            using (var db = new ApplicationDbContext())
            {
                IQueryable<TB_CONTROL_RECEPCION_ANIO> query = db.TB_CONTROL_RECEPCION_ANIO;

                query = query.Where(item => item.fecha_ini.Date == fecha_ini.Date && item.fecha_fin.Date == fecha_fin.Date);

                list = query.OrderBy(c => c.id).ToList();
            }
            return list;
        }

        public IEnumerable<TB_CONTROL_PROCESADAS_DIA> GetProcesadasDia(DateTime fecha_ini, DateTime fecha_fin)
        {
            var list = new List<TB_CONTROL_PROCESADAS_DIA>();

            using (var db = new ApplicationDbContext())
            {
                IQueryable<TB_CONTROL_PROCESADAS_DIA> query = db.TB_CONTROL_PROCESADAS_DIA;

                query = query.Where(item => item.fecha_ini.Date == fecha_ini.Date && item.fecha_fin.Date == fecha_fin.Date);

                list = query.OrderBy(c => c.id).ToList();
            }
            return list;
        }

        public IEnumerable<TB_HISTORIAL_CARGAS> GetAllCargasCSV(DateTime ingreso)
        {
            var result = new List<TB_HISTORIAL_CARGAS>();

            using (var db = new ApplicationDbContext())
            {
                IQueryable<TB_HISTORIAL_CARGAS> query = db.TB_HISTORIAL_CARGAS.Where(item => item.fecha.Date >= ingreso.Date);

                result = query.OrderByDescending(m => m.fecha).ToList();

                return result;

            }
        }

    }
}
