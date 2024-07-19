using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

public partial class MovilBusiness5StdContext : DbContext
{
    public MovilBusiness5StdContext()
    {
    }

    public MovilBusiness5StdContext(DbContextOptions<MovilBusiness5StdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<ComprasDetalle> ComprasDetalles { get; set; }

    public virtual DbSet<ComprasProducto> ComprasProductos { get; set; }

    public virtual DbSet<ComprasProductosDetalle> ComprasProductosDetalles { get; set; }

    public virtual DbSet<LotesFermentacion> LotesFermentacions { get; set; }

    public virtual DbSet<LotesFermentacionDetalle> LotesFermentacionDetalles { get; set; }

    public virtual DbSet<LotesLimpieza> LotesLimpiezas { get; set; }

    public virtual DbSet<LotesLimpiezaDetalle> LotesLimpiezaDetalles { get; set; }

    public virtual DbSet<LotesSecadoMaquina> LotesSecadoMaquinas { get; set; }

    public virtual DbSet<LotesSecadoMaquinaDetalle> LotesSecadoMaquinaDetalles { get; set; }

    public virtual DbSet<LotesSecadoNatural> LotesSecadoNaturals { get; set; }

    public virtual DbSet<LotesSecadoNaturalDetalle> LotesSecadoNaturalDetalles { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<RecepcionesCompra> RecepcionesCompras { get; set; }

    public virtual DbSet<RecepcionesComprasDetalle> RecepcionesComprasDetalles { get; set; }

    public virtual DbSet<RecepcionesProducto> RecepcionesProductos { get; set; }

    public virtual DbSet<RecepcionesProductosDetalle> RecepcionesProductosDetalles { get; set; }

    public virtual DbSet<Representante> Representantes { get; set; }

    public virtual DbSet<UsuarioSistema> UsuarioSistemas { get; set; }

    public virtual DbSet<VwComprasProducto> VwComprasProductos { get; set; }

    public virtual DbSet<VwComprasProductosDetalle> VwComprasProductosDetalles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Compra>(entity =>
        {
            entity.ToTable(tb =>
                {
                    tb.HasComment("Maestra de Compras");
                    tb.HasTrigger("tg_MD_REPL_Compras_IUDV7");
                    tb.HasTrigger("trg_ComprasIOI");
                });

            entity.Property(e => e.ComSecuencia).HasComment("Secuencia");
            entity.Property(e => e.RepCodigo).HasComment("Codigo de Representante");
            entity.Property(e => e.Cldcedula).HasComment("Cédula");
            entity.Property(e => e.CliId).HasComment("ID Cliente");
            entity.Property(e => e.ComCantidadCanastos)
                .HasDefaultValueSql("((0))")
                .HasComment("Cantidad canastos");
            entity.Property(e => e.ComCantidadDetalle).HasComment("Cantidad detalle");
            entity.Property(e => e.ComCantidadImpresion).HasComment("Cantidad impresión");
            entity.Property(e => e.ComEstatus).HasComment("Estatus");
            entity.Property(e => e.ComFecha).HasComment("Fecha");
            entity.Property(e => e.ComFechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Fecha Creacion/Actualizacion ");
            entity.Property(e => e.ComNcf).HasComment("NCF");
            entity.Property(e => e.ComReferencia).HasComment("Referencia");
            entity.Property(e => e.ComTotal).HasComment("Total");
            entity.Property(e => e.ConId).HasComment("ID condición de pago");
            entity.Property(e => e.CuaSecuencia).HasComment("Secuencia");
            entity.Property(e => e.DepSecuencia).HasComment("Secuencia");
            entity.Property(e => e.MbVersion).HasComment("Version de MovilBusiness");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("Identificador Unico");
            entity.Property(e => e.UsuInicioSesion).HasComment("Usuario que creo o modifico el registro");
            entity.Property(e => e.VisSecuencia).HasComment("Secuencia");
        });

        modelBuilder.Entity<ComprasDetalle>(entity =>
        {
            entity.ToTable("ComprasDetalle", tb =>
                {
                    tb.HasComment("Detalle de Compras");
                    tb.HasTrigger("tg_MD_REPL_ComprasDetalle_IUDV7");
                    tb.HasTrigger("trg_ComprasDetalleIOI");
                });

            entity.Property(e => e.RepCodigo).HasComment("Codigo de Representante");
            entity.Property(e => e.ComSecuencia).HasComment("Secuencia");
            entity.Property(e => e.ComPosicion).HasComment("Posición");
            entity.Property(e => e.ComAdValorem).HasComment("Impuesto AdValorem");
            entity.Property(e => e.ComCantidad).HasComment("Cantidad");
            entity.Property(e => e.ComCantidadDetalle).HasComment("Cantidad detalle");
            entity.Property(e => e.ComDescuento).HasComment("Descuento");
            entity.Property(e => e.ComFechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Fecha Creacion/Actualizacion ");
            entity.Property(e => e.ComItbis).HasComment("Itbis");
            entity.Property(e => e.ComPrecio).HasComment("Precio");
            entity.Property(e => e.ComSelectivo).HasComment("Selectivo");
            entity.Property(e => e.ComTotalDescuento).HasComment("Total descuento");
            entity.Property(e => e.ComTotalItbis).HasComment("Total Itbis");
            entity.Property(e => e.ComindicadorOferta)
                .HasDefaultValueSql("((0))")
                .HasComment("Indicador oferta");
            entity.Property(e => e.CxcDocumento).HasComment("Número de documento cuentas por cobrar");
            entity.Property(e => e.ProId).HasComment("ID Producto");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("Identificador Unico");
            entity.Property(e => e.UsuInicioSesion).HasComment("Usuario que creo o modifico el registro");
        });

        modelBuilder.Entity<ComprasProducto>(entity =>
        {
            entity.HasKey(e => new { e.ComSecuencia, e.RepCodigo }).HasName("PK__ComprasP__F29A5E899E00CD55");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("tg_MD_REPL_ComprasProductos_IUDV7");
                    tb.HasTrigger("trg_ComprasProductosCambiarEstatus");
                });

            entity.Property(e => e.ComEstatusRecepcion).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<ComprasProductosDetalle>(entity =>
        {
            entity.HasKey(e => new { e.RepCodigo, e.ComSecuencia, e.ComPosicion }).HasName("PK__ComprasP__A54C7D876A365F90");

            entity.ToTable("ComprasProductosDetalle", tb => tb.HasTrigger("tg_MD_REPL_ComprasProductosDetalle_IUDV7"));
        });

        modelBuilder.Entity<LotesFermentacion>(entity =>
        {
            entity.HasKey(e => e.LotFermentacion).HasName("PK__LotesFer__6D556F5F91FC3215");
        });

        modelBuilder.Entity<LotesFermentacionDetalle>(entity =>
        {
            entity.HasKey(e => new { e.LotFermentacion, e.LotFermentacionSecuencia }).HasName("PK__LotesFer__303FB8CC9F3BA22D");
        });

        modelBuilder.Entity<LotesLimpieza>(entity =>
        {
            entity.HasKey(e => e.LotLimpieza).HasName("PK__LotesLim__5CA6223F1ECA25A2");
        });

        modelBuilder.Entity<LotesLimpiezaDetalle>(entity =>
        {
            entity.HasKey(e => new { e.LotLimpieza, e.LotPosicion }).HasName("PK__LotesLim__9AFFBC61504C57FC");
        });

        modelBuilder.Entity<LotesSecadoMaquina>(entity =>
        {
            entity.HasKey(e => e.LotSecadoMaquina).HasName("PK__LotesSec__65FF21422BA12039");
        });

        modelBuilder.Entity<LotesSecadoMaquinaDetalle>(entity =>
        {
            entity.HasKey(e => new { e.LotSecadoMaquina, e.LotPosicion }).HasName("PK__LotesSec__A3A6BF1C01BD50E8");
        });

        modelBuilder.Entity<LotesSecadoNatural>(entity =>
        {
            entity.HasKey(e => e.LotSecadoManual).HasName("PK__LotesSec__D5FE91FDEE271836");
        });

        modelBuilder.Entity<LotesSecadoNaturalDetalle>(entity =>
        {
            entity.HasKey(e => new { e.LotSecadoManual, e.LotPosicion }).HasName("PK__LotesSec__13A70FA36073BF47");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable(tb =>
                {
                    tb.HasComment("Maestro de Productos");
                    tb.HasTrigger("tg_MD_REPL_Productos_IUDV7");
                    tb.HasTrigger("trg_ProductosIOI");
                });

            entity.Property(e => e.ProId)
                .ValueGeneratedNever()
                .HasComment("ID Producto");
            entity.Property(e => e.Cat1Id).HasComment("ID Categoria 1");
            entity.Property(e => e.Cat2Id).HasComment("ID Categoria 2  fk:Categoria2");
            entity.Property(e => e.Cat3Id).HasComment("ID Categoria 3  fk:Categoria3");
            entity.Property(e => e.LinId).HasComment("ID Linea fk:Lineas");
            entity.Property(e => e.ProAdValorem)
                .HasDefaultValueSql("((0))")
                .HasComment("Impuesto AdValorem");
            entity.Property(e => e.ProCantidad).HasComment("Cantidad");
            entity.Property(e => e.ProCargoDistribucion).HasComment("Cargo por Distribucion");
            entity.Property(e => e.ProCodigo).HasComment("Código");
            entity.Property(e => e.ProCodigoDescuento).HasComment("Código descuento");
            entity.Property(e => e.ProDatos1).HasComment("Datos 1");
            entity.Property(e => e.ProDatos2).HasComment("Datos 2");
            entity.Property(e => e.ProDatos3).HasComment("Datos 3");
            entity.Property(e => e.ProDescripcion).HasComment("Descripcion ");
            entity.Property(e => e.ProDescripcion1).HasComment("Descripcion 1");
            entity.Property(e => e.ProDescripcion2).HasComment("Descripción 2");
            entity.Property(e => e.ProDescripcion3).HasComment("Descripción");
            entity.Property(e => e.ProFechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Fecha Creacion/Actualizacion ");
            entity.Property(e => e.ProImg).HasComment("Imagen producto");
            entity.Property(e => e.ProIndicadorDetalle)
                .HasDefaultValueSql("((0))")
                .HasComment("Indicador detalle");
            entity.Property(e => e.ProItbis).HasComment("Impuesto");
            entity.Property(e => e.ProPeso).HasComment("Peso");
            entity.Property(e => e.ProPrecio).HasComment("Precio");
            entity.Property(e => e.ProPrecio2).HasComment("Precio");
            entity.Property(e => e.ProPrecio3).HasComment("Precio 3 Cuando no se usa LipCodigo");
            entity.Property(e => e.ProPrecioMin).HasComment("Precio mínimo");
            entity.Property(e => e.ProReferencia).HasComment("Codigo de Barra");
            entity.Property(e => e.ProSelectivo).HasComment("Selectivo");
            entity.Property(e => e.ProUnidades).HasComment("Unidades por Producto");
            entity.Property(e => e.ProVolumen).HasComment("Volumen");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("Identificador Unico");
            entity.Property(e => e.SecCodigo).HasComment("Codigo Sector  fk:Sectores");
            entity.Property(e => e.UnmCodigo).HasComment("Codigo Unidad de Medida fk:UsosMultiples Grupo:UnmCodigo");
            entity.Property(e => e.UsuInicioSesion)
                .HasDefaultValueSql("(suser_sname())")
                .HasComment("Usuario que creo o modifico el registro");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.ProCodigo).HasName("PK__Proveedo__90746C226DFCD3C2");

            entity.ToTable(tb => tb.HasTrigger("tg_MD_REPL_Proveedores_IUDV7"));
        });

        modelBuilder.Entity<RecepcionesCompra>(entity =>
        {
            entity.ToTable(tb =>
                {
                    tb.HasTrigger("tg_MD_REPL_RecepcionesCompras_IUDV7");
                    tb.HasTrigger("trg_RecepcionesComprasIOI");
                });

            entity.Property(e => e.RecEstatusErp).IsFixedLength();
        });

        modelBuilder.Entity<RecepcionesComprasDetalle>(entity =>
        {
            entity.ToTable("RecepcionesComprasDetalle", tb =>
                {
                    tb.HasTrigger("tg_MD_REPL_RecepcionesComprasDetalle_IUDV7");
                    tb.HasTrigger("trg_RecepcionesComprasDetalleIOI");
                });

            entity.Property(e => e.RecFechaActualizacion).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Representante>(entity =>
        {
            entity.HasKey(e => e.RepCodigo).HasName("PK_Usuarios");

            entity.ToTable(tb =>
                {
                    tb.HasComment("Maestra de Representantes ");
                    tb.HasTrigger("tg_MD_REPL_Representantes_IUDV7");
                    tb.HasTrigger("trg_RepresentantesIOI");
                });

            entity.Property(e => e.RepCodigo).HasComment("Codigo de Representante");
            entity.Property(e => e.AlmId).HasComment("ID de almacén");
            entity.Property(e => e.EquId).HasComment("ID equipo");
            entity.Property(e => e.RepCargo).HasComment("Cargo");
            entity.Property(e => e.RepClasificacion).HasComment("Clasificación");
            entity.Property(e => e.RepClave).HasComment("Clave");
            entity.Property(e => e.RepClaveSync).HasComment("Clave Sync");
            entity.Property(e => e.RepCobros).HasComment("Cobros");
            entity.Property(e => e.RepDeposito).HasComment("Depósito");
            entity.Property(e => e.RepDevoluciones).HasComment("Devoluciones");
            entity.Property(e => e.RepDivision).HasComment("División");
            entity.Property(e => e.RepEntrega).HasComment("Entrega");
            entity.Property(e => e.RepEstatus).HasComment("Estatus");
            entity.Property(e => e.RepFechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Fecha Creacion/Actualizacion ");
            entity.Property(e => e.RepFechaUltimaActualizacion).HasComment("Fecha última actualización");
            entity.Property(e => e.RepIndicadorRutaVisitasFecha).HasComment("Fecha indicador ruta visita");
            entity.Property(e => e.RepIndicadorSupervisor).HasComment("Indicador supervisor");
            entity.Property(e => e.RepIndicadorVenta).HasComment("Indicador venta");
            entity.Property(e => e.RepInventarioVentas).HasComment("Inventario ventas");
            entity.Property(e => e.RepInvestigacion).HasComment("Investigación");
            entity.Property(e => e.RepLicencia)
                .HasDefaultValueSql("(newid())")
                .HasComment("Licencia");
            entity.Property(e => e.RepM1).HasComment("M1 Representantes");
            entity.Property(e => e.RepM2).HasComment("M2 Representantes");
            entity.Property(e => e.RepM3).HasComment("M3 Representantes");
            entity.Property(e => e.RepM4).HasComment("M4 Representantes");
            entity.Property(e => e.RepM5).HasComment("M5 Representantes");
            entity.Property(e => e.RepNombre).HasComment("Nombre");
            entity.Property(e => e.RepPedidos).HasComment("Pedidos");
            entity.Property(e => e.RepSupervisor).HasComment("Representante supervisor");
            entity.Property(e => e.RepTelefono1).HasComment("Teléfono 1");
            entity.Property(e => e.RepTelefono2).HasComment("Teléfono 2");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("Identificador Unico");
            entity.Property(e => e.RutId).HasComment("ID ruta");
            entity.Property(e => e.UsuInicioSesion).HasComment("Usuario que creo o modifico el registro");
            entity.Property(e => e.VehId).HasComment("ID vehiculo");
            entity.Property(e => e.ZonId).HasComment("ID zona");
        });

        modelBuilder.Entity<UsuarioSistema>(entity =>
        {
            entity.HasKey(e => e.UsuInicioSesion).HasName("PK16");

            entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<VwComprasProducto>(entity =>
        {
            entity.ToView("vwComprasProductos");
        });

        modelBuilder.Entity<VwComprasProductosDetalle>(entity =>
        {
            entity.ToView("vwComprasProductosDetalle");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
