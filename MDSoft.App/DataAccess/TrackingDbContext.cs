using Tracking.Utilidades;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Tracking.Modelos;

namespace Tracking.DataAccess
{
    public class TrackingDbContext : DbContext
    {

        public DbSet<SettingsData> SettingData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDb = $"Filename={ConexionDb.DevolverRuta("TrackSettings.db")}";
            optionsBuilder.UseSqlite(conexionDb);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SettingsData>(entity =>
            {
                entity.HasKey(c => c.Id);
            });
         }


    }
}
