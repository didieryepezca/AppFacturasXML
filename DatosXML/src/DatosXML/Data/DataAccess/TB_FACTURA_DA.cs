using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using DatosXML.Models.Entities;
using DatosXML.Models;

namespace DatosXML.Data
{
    public class TB_FACTURA_DA
    {

        public int Insert(TB_FACTURA_XML TB_FACTURA_XML)
        {
            var result = 0;

            using (var db = new ApplicationDbContext())
            {
                db.Add(TB_FACTURA_XML);
                result = db.SaveChanges();

            }
            return result;

        }


        public IEnumerable<TB_FACTURA_XML> GetXML(DateTime ingreso)
        {
            var result = new List<TB_FACTURA_XML>();

            using (var db = new ApplicationDbContext())
            {
                IQueryable<TB_FACTURA_XML> query = db.TB_FACTURA_XML.Where(item => item.FECHA_INGRESO.Date == ingreso.Date);

                result = query.OrderByDescending(m => m.FECHA_INGRESO).ToList();

                return result;

            }
        }

        public IEnumerable<TB_FACTURA_XML> GetFacturasFechaIngreso(DateTime ingreso)
        {
            var list = new List<TB_FACTURA_XML>();

            using (var db = new ApplicationDbContext())
            {

                IQueryable<TB_FACTURA_XML> query = db.TB_FACTURA_XML.Where(item => item.FECHA_INGRESO.Date == ingreso.Date);

                list = query.OrderByDescending(m => m.FECHA_INGRESO).ToList();

                return list;
            }

        }

        public int BorrarDiario(DateTime fecha)
        {
            var result = 0;
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    SqlParameter fechaD = new SqlParameter();
                    fechaD.ParameterName = "@FEC";
                    fechaD.Value = fecha;
                    fechaD.SqlDbType = SqlDbType.Date;
                    fechaD.Direction = System.Data.ParameterDirection.Input;                

                    result = db.Database.ExecuteSqlCommand("BORRAR_DIARIO @FEC ", fechaD);
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            return result;
        }


        public int BorrarDiarioNoLeidas(DateTime fecha)
        {
            var result = 0;
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    SqlParameter fechaD = new SqlParameter();
                    fechaD.ParameterName = "@FECHA";
                    fechaD.Value = fecha;
                    fechaD.SqlDbType = SqlDbType.Date;
                    fechaD.Direction = System.Data.ParameterDirection.Input;

                    result = db.Database.ExecuteSqlCommand("BORRAR_DIARIO_NO_LEIDAS @FECHA ", fechaD);
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            return result;
        }


        public int InsertFacturaNoLeida(TB_FACTURA_NO_LEIDA TB_FACTURA_NO_LEIDA)
        {
            var result = 0;

            using (var db = new ApplicationDbContext())
            {
                db.Add(TB_FACTURA_NO_LEIDA);
                result = db.SaveChanges();

            }
            return result;

        }



        public IEnumerable<TB_FACTURA_NO_LEIDA> GetFacturasNoLeidas(DateTime ingreso)
        {
            var result = new List<TB_FACTURA_NO_LEIDA>();

            using (var db = new ApplicationDbContext())
            {
                IQueryable<TB_FACTURA_NO_LEIDA> query = db.TB_FACTURA_NO_LEIDA.Where(item => item.FECHA_INGRESO.Date == ingreso.Date);

                result = query.OrderByDescending(m => m.FECHA_INGRESO).ToList();

                return result;

            }
        }



    }
}
