using Tracking.Utilidades;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Tracking.Modelos;

namespace Tracking.DataAccess
{
    public class VentaDbContext : DbContext
    {

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetalleCompras { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Recepcion> Recepciones { get; set; }
        public DbSet<RecepcionesCompra> RecepcionesCompra { get; set; }
        public DbSet<RecepcionDetalle> RecepcionDetalles { get; set; }
        public DbSet<RecepcionesComprasDetalle> RecepcionesComprasDetalles { get; set; }
        public DbSet<ComprasProducto> CompraProducto { get; set; }
        public DbSet<Representante> Representante { get; set; }
        public DbSet<Proveedore> Proveedore { get; set; }
        public DbSet<ComprasProductosDetalle> CompraProductoDetalle { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDb = $"Filename={ConexionDb.DevolverRuta("venta0.db")}";
            optionsBuilder.UseSqlite(conexionDb);
            //optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(c => c.IdCategoria);
                entity.Property(c => c.IdCategoria).IsRequired().ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(p => p.ProCodigo);
                entity.Property(p => p.ProId).IsRequired().ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Producto>().HasData(
                 new Producto
                 {
                     ProId = 1,
                     ProCodigo = "111111",
                     ProDescripcion = "laptop samsung book pro",
                     ProCantidad = 20,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 2500
                 },
                 new Producto
                 {
                     ProId = 2,
                     ProCodigo = "222222",
                     ProDescripcion = "laptop lenovo idea pad",
                     ProCantidad = 30,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 2100

                 },
                 new Producto
                 {
                     ProId = 3,
                     ProCodigo = "333333",
                     ProDescripcion = "laptop asus zenbook duo",
                     ProCantidad = 30,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 2100
                 },
                 new Producto
                 {
                     ProId = 4,
                     ProCodigo = "444444",
                     ProDescripcion = "monitor teros gaming te-2",
                     ProCantidad = 25,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 1050
                 },
                 new Producto
                 {
                     ProId = 5,
                     ProCodigo = "555555",
                     ProDescripcion = "monitor samsung curvo",
                     ProCantidad = 15,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 1400
                 },
                 new Producto
                 {
                     ProId = 6,
                     ProCodigo = "666666",
                     ProDescripcion = "monitor huawei gamer",
                     ProCantidad = 18,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 1350
                 },
                 new Producto
                 {
                     ProId = 7,
                     ProCodigo = "777777",
                     ProDescripcion = "teclado seisen gamer",
                     ProCantidad = 30,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 800
                 },
                 new Producto
                 {
                     ProId = 8,
                     ProCodigo = "888888",
                     ProDescripcion = "teclado antryx gamer",
                     ProCantidad = 20,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 1000
                 },
                 new Producto
                 {
                     ProId = 9,
                     ProCodigo = "999999",
                     ProDescripcion = "teclado logitech",
                     ProCantidad = 20,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 1000
                 },
                 new Producto
                 {
                     ProId = 10,
                     ProCodigo = "101010",
                     ProDescripcion = "auricular logitech gamer",
                     ProCantidad = 20,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 800
                 },
                 new Producto
                 {
                     ProId = 11,
                     ProCodigo = "111110",
                     ProDescripcion = "auricular hyperx gamer",
                     ProCantidad = 20,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 680
                 },
                 new Producto
                 {
                     ProId = 12,
                     ProCodigo = "121212",
                     ProDescripcion = "auricular redragon rgb",
                     ProCantidad = 25,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 950
                 },
                 new Producto
                 {
                     ProId = 13,
                     ProCodigo = "131313",
                     ProDescripcion = "memoria kingston rgb",
                     ProCantidad = 20,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 200
                 },
                 new Producto
                 {
                     ProId = 14,
                     ProCodigo = "141414",
                     ProDescripcion = "ventilador cooler master",
                     ProCantidad = 20,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 200
                 },
                 new Producto
                 {
                     ProId = 15,
                     ProCodigo = "151515",
                     ProDescripcion = "mini ventilador lenono",
                     ProCantidad = 25,
                     Rowguid = Guid.NewGuid(),
                     ProPrecio = 260
                 }
                 );

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(v => v.IdVenta);
                entity.Property(v => v.IdVenta).IsRequired().ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Id).IsRequired().ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Compra>().HasData(
                new Compra { Id = 1, CodigoCompra = "0101010101", Fecha = DateTime.Now, IdRepresentante = 1, IdVendedor = 2, Peso = 100, Cantidad = 1 },
                new Compra { Id = 2, CodigoCompra = "0101010102", Fecha = DateTime.Now, IdRepresentante = 1, IdVendedor = 2, Peso = 100, Cantidad = 75 },
                new Compra { Id = 3, CodigoCompra = "0101010103", Fecha = DateTime.Now, IdRepresentante = 1, IdVendedor = 2, Peso = 100, Cantidad = 20 }
                );

            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.HasKey(dv => dv.Id);
                entity.Property(dv => dv.Id).IsRequired().ValueGeneratedOnAdd();
                entity.HasOne(dv => dv.Compra).WithMany(v => v.detalleCompra)
                .HasForeignKey(dv => dv.IdCompra);
                // entity.HasOne(dv => dv.Producto).WithMany(p => p.detalleCompra)
                //.HasForeignKey(p => p.ProId);
            });

            modelBuilder.Entity<DetalleCompra>().HasData(
                new DetalleCompra
                {
                    Id = 1,
                    ComReferencia = "0101010101",
                    Estatus = 1,
                    IdCompra = 1,
                    Peso = 100,
                },
               new DetalleCompra
               {
                   Id = 2,
                   ComReferencia = "0101010102",
                   Estatus = 1,
                   IdCompra = 2,
                   Peso = 100,
               });

            modelBuilder.Entity<Recepcion>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Id).IsRequired().ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Recepcion>().HasData(
                new Recepcion { Id = 1, CodigoCompra = "0101010101", FechaRecepcion = DateTime.Now, IdRepresentante = 1, IdVendedor = 2, TipoLote = 1, LotFermentacion = 100, LotSecadoMaquina = null, PesoCompra = 100, PesoRecibido = 75 },
                new Recepcion { Id = 2, CodigoCompra = "0101010102", FechaRecepcion = DateTime.Now, IdRepresentante = 1, IdVendedor = 2, TipoLote = 2, LotFermentacion = null, LotSecadoMaquina = 02, PesoCompra = 100, PesoRecibido = 75 },
                new Recepcion { Id = 3, CodigoCompra = "0101010103", FechaRecepcion = DateTime.Now, IdRepresentante = 1, IdVendedor = 2, TipoLote = 2, LotFermentacion = null, LotSecadoMaquina = 03, PesoCompra = 100, PesoRecibido = 75 }
                );

            modelBuilder.Entity<RecepcionDetalle>(entity =>
            {
                entity.HasKey(dv => dv.RecepcionDetalleId);
                entity.Property(dv => dv.RecepcionDetalleId).IsRequired().ValueGeneratedOnAdd();
                entity.HasOne(dv => dv.Recepcion).WithMany(v => v.recepcionDetalle)
                .HasForeignKey(dv => dv.RecepcionId);

                //entity.HasOne(dv => dv.RefProducto).WithMany(p => p.RefDetalleVenta)
                //.HasForeignKey(p => p.ProId);
            });


            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.HasKey(dv => dv.IdDetalleVenta);
                entity.Property(dv => dv.IdDetalleVenta).IsRequired().ValueGeneratedOnAdd();
                entity.HasOne(dv => dv.RefVenta).WithMany(v => v.RefDetalleVenta)
                .HasForeignKey(dv => dv.IdVenta);
                //entity.HasOne(dv => dv.RefProducto).WithMany(p => p.RefDetalleVenta)
               //.HasForeignKey(p => p.ProId);
            });

            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { IdCategoria = 1, Nombre = "Fermentación" },
                new Categoria { IdCategoria = 2, Nombre = "Secado Natural" }
                );


         }


    }
}
