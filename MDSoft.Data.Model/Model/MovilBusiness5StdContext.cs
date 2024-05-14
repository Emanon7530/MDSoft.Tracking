using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking;

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

    public virtual DbSet<Representante> Representantes { get; set; }

    public virtual DbSet<UsuarioSistema> UsuarioSistemas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer("Data Source=AGTDEVL1019;Initial Catalog=MovilBusiness5STD;Persist Security Info=True;User ID=sa;Password=manonram;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Compra>(entity =>
        {
            entity.ToTable(tb =>
                {
                    tb.HasComment("Maestra de Compras");
                    tb.HasTrigger("tg_ComprasEstatusActualizar");
                    tb.HasTrigger("tg_MD_Compras_IUD");
                    tb.HasTrigger("tg_MD_REPL_Compras_IUDV9");
                    tb.HasTrigger("trg_ComprasCambiarEstatus");
                    tb.HasTrigger("trg_ComprasIOI");
                });

            entity.HasIndex(e => e.Rowguid, "MSmerge_index_827527123")
                .IsUnique()
                .HasFillFactor(90);

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
                    tb.HasTrigger("tg_MD_ComprasDetalle_IUD");
                    tb.HasTrigger("tg_MD_REPL_ComprasDetalle_IUDV9");
                    tb.HasTrigger("trg_ComprasDetalleIOI");
                });

            entity.HasIndex(e => e.Rowguid, "MSmerge_index_2011531341")
                .IsUnique()
                .HasFillFactor(90);

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
            entity.ToTable(tb => tb.HasTrigger("tg_MD_REPL_ComprasProductos_IUDV7"));
        });

        modelBuilder.Entity<ComprasProductosDetalle>(entity =>
        {
            entity.ToTable("ComprasProductosDetalle", tb => tb.HasTrigger("tg_MD_REPL_ComprasProductosDetalle_IUDV7"));

            entity.Property(e => e.ComReferencia).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.ComprasProducto).WithMany(p => p.ComprasProductosDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComprasProductosDetalle_ComprasProductos");
        });

        modelBuilder.Entity<LotesFermentacionDetalle>(entity =>
        {
            entity.HasOne(d => d.LotFermentacionNavigation).WithMany(p => p.LotesFermentacionDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LotesFermentacionDetalle_LotesFermentacion");
        });

        modelBuilder.Entity<LotesLimpiezaDetalle>(entity =>
        {
            entity.HasOne(d => d.LotLimpiezaNavigation).WithMany(p => p.LotesLimpiezaDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LotesLimpiezaDetalle_LotesLimpieza");
        });

        modelBuilder.Entity<LotesSecadoMaquinaDetalle>(entity =>
        {
            entity.HasOne(d => d.LotSecadoMaquinaNavigation).WithMany(p => p.LotesSecadoMaquinaDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LotesSecadoMaquinaDetalle_LotesSecadoMaquina");
        });

        modelBuilder.Entity<LotesSecadoNatural>(entity =>
        {
            entity.HasKey(e => e.LotSecadoManual).HasName("PK_LotesSecado");
        });

        modelBuilder.Entity<LotesSecadoNaturalDetalle>(entity =>
        {
            entity.HasKey(e => new { e.LotSecadoManual, e.LotPosicion }).HasName("PK_LotesSecadoDetalle");

            entity.HasOne(d => d.LotSecadoManualNavigation).WithMany(p => p.LotesSecadoNaturalDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LotesSecadoManualDetalle_LotesSecadoManual");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable(tb =>
                {
                    tb.HasComment("Maestro de Productos");
                    tb.HasTrigger("tg_MD_Productos_IUD");
                    tb.HasTrigger("tg_MD_REPL_Productos_IUDV9");
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
            entity.ToTable(tb => tb.HasTrigger("tg_MD_REPL_Proveedores_IUDV7"));

            entity.Property(e => e.ProCalle).HasDefaultValueSql("('')");
            entity.Property(e => e.ProCasa).HasDefaultValueSql("('')");
            entity.Property(e => e.ProContacto).HasDefaultValueSql("('')");
            entity.Property(e => e.ProFechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ProFuente).HasDefaultValueSql("('ERP')");
            entity.Property(e => e.ProTelefono).HasDefaultValueSql("('')");
            entity.Property(e => e.ProUrbanizacion).HasDefaultValueSql("('')");
            entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<RecepcionesCompra>(entity =>
        {
            entity.HasKey(e => e.RecSecuencia).HasName("PK_RecepcionCompras");

            entity.Property(e => e.RecSecuencia).ValueGeneratedNever();
        });

        modelBuilder.Entity<RecepcionesComprasDetalle>(entity =>
        {
            entity.HasKey(e => new { e.RecSecuencia, e.RecPosicion }).HasName("PK_RecepcionComprasDetalle");

            entity.HasOne(d => d.RecSecuenciaNavigation).WithMany(p => p.RecepcionesComprasDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecepcionComprasDetalle_RecepcionCompras");
        });

        modelBuilder.Entity<Representante>(entity =>
        {
            entity.HasKey(e => e.RepCodigo).HasName("PK_Usuarios");

            entity.ToTable(tb =>
                {
                    tb.HasComment("Maestra de Representantes ");
                    tb.HasTrigger("tg_MD_REPL_Representantes_IUDV9");
                    tb.HasTrigger("tg_MD_Representantes_IUD");
                    tb.HasTrigger("trg_RepresentantesIOI");
                    tb.HasTrigger("trg_RepresentantesRutaAsignar");
                });

            entity.HasIndex(e => e.Rowguid, "index_1428200138")
                .IsUnique()
                .HasFillFactor(90);

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

            entity.ToTable("UsuarioSistema", tb =>
                {
                    tb.HasComment("Maestra de Usuario del Sistema");
                    tb.HasTrigger("tg_MD_UsuarioSistema_IUD");
                    tb.HasTrigger("tg_usuarios_claves");
                });

            entity.Property(e => e.UsuInicioSesion).HasComment("Usuario que creo o modifico el registro");
            entity.Property(e => e.CliId).HasComment("ID Cliente");
            entity.Property(e => e.RepCodigo).HasComment("Codigo de Representante");
            entity.Property(e => e.RolId).HasComment("ID Rol");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("Identificador Unico");
            entity.Property(e => e.UsuApellidos).HasComment("Apellidos usuario");
            entity.Property(e => e.UsuCedula).HasComment("Cédula usuario");
            entity.Property(e => e.UsuClave).HasComment("Clave");
            entity.Property(e => e.UsuCorreoElectronico).HasComment("Correo electrónico usuario");
            entity.Property(e => e.UsuDepartamento).HasComment("Departamento");
            entity.Property(e => e.UsuEstatus).HasComment("Estatus");
            entity.Property(e => e.UsuFechaActualizacion).HasComment("Fecha Creacion/Actualizacion ");
            entity.Property(e => e.UsuFiltrarClientes).HasComment("Filtrar clientes");
            entity.Property(e => e.UsuInstitucion).HasComment("Institución");
            entity.Property(e => e.UsuNombres).HasComment("Nombres usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
