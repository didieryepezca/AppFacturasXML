using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DatosXML.Models;
using DatosXML.Models.Entities;

namespace DatosXML.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

            
            

        }

        public ApplicationDbContext()
        {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Server=172.17.1.51;" +
                   "Database=FACTURAS_XML;" +
                   "Trusted_Connection=True;" +
                   "MultipleActiveResultSets=True");
        }

        public virtual DbSet<TB_FACTURA_XML> TB_FACTURA_XML { get; set; }
        public virtual DbSet<TB_FACTURA_NO_LEIDA> TB_FACTURA_NO_LEIDA { get; set; }
        public virtual DbSet<TB_PRINCIPALES_INDICADORES> TB_PRINCIPALES_INDICADORES { get; set; }
        public virtual DbSet<TB_CONTROL_RECEPCION> TB_CONTROL_RECEPCION { get; set; }
        public virtual DbSet<TB_CONTROL_RECEPCION_DIA> TB_CONTROL_RECEPCION_DIA { get; set; }
        public virtual DbSet<TB_DISTRIBUCION_RECHAZOS> TB_DISTRIBUCION_RECHAZOS { get; set; }
        public virtual DbSet<TB_CONTROL_RECEPCION_ANIO> TB_CONTROL_RECEPCION_ANIO { get; set; }
        public virtual DbSet<TB_HISTORIAL_CARGAS> TB_HISTORIAL_CARGAS { get; set; }
        public virtual DbSet<TB_CONTROL_PROCESADAS_DIA> TB_CONTROL_PROCESADAS_DIA { get; set; }
    }
}
