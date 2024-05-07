using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tracking.Modelos;


/// <summary>
/// Maestro de Productos
/// </summary>
[Index("Rowguid", Name = "IDXProductosrowguid", IsUnique = true)]
public partial class Producto
{
    /// <summary>
    /// ID Producto
    /// </summary>
    [Key]
    [Column("ProID")]
    public int ProId { get; set; }

    /// <summary>
    /// ID Linea fk:Lineas
    /// </summary>
    [Column("LinID")]
    public int? LinId { get; set; }

    /// <summary>
    /// Descripcion 
    /// </summary>
    [StringLength(200)]
    [Unicode(false)]
    public string? ProDescripcion { get; set; }

    /// <summary>
    /// Precio
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProPrecio { get; set; }

    /// <summary>
    /// Precio
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProPrecio2 { get; set; }

    /// <summary>
    /// Precio 3 Cuando no se usa LipCodigo
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProPrecio3 { get; set; }

    /// <summary>
    /// Precio mínimo
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProPrecioMin { get; set; }

    /// <summary>
    /// Código
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? ProCodigo { get; set; }

    /// <summary>
    /// Codigo de Barra
    /// </summary>
    [StringLength(120)]
    [Unicode(false)]
    public string? ProReferencia { get; set; }

    /// <summary>
    /// Unidades por Producto
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProUnidades { get; set; }

    /// <summary>
    /// Cantidad
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProCantidad { get; set; }

    /// <summary>
    /// ID Categoria 1
    /// </summary>
    [Column("Cat1ID")]
    public int? Cat1Id { get; set; }

    /// <summary>
    /// ID Categoria 2  fk:Categoria2
    /// </summary>
    [Column("Cat2ID")]
    public int? Cat2Id { get; set; }

    /// <summary>
    /// ID Categoria 3  fk:Categoria3
    /// </summary>
    [Column("Cat3ID")]
    public int? Cat3Id { get; set; }

    /// <summary>
    /// Descripcion 1
    /// </summary>
    [StringLength(200)]
    [Unicode(false)]
    public string? ProDescripcion1 { get; set; }

    /// <summary>
    /// Descripción 2
    /// </summary>
    [StringLength(200)]
    [Unicode(false)]
    public string? ProDescripcion2 { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>
    [StringLength(200)]
    [Unicode(false)]
    public string? ProDescripcion3 { get; set; }

    /// <summary>
    /// Datos 1
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? ProDatos1 { get; set; }

    /// <summary>
    /// Datos 2
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? ProDatos2 { get; set; }

    /// <summary>
    /// Datos 3
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? ProDatos3 { get; set; }

    /// <summary>
    /// Impuesto
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProItbis { get; set; }

    /// <summary>
    /// Selectivo
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProSelectivo { get; set; }

    /// <summary>
    /// Impuesto AdValorem
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProAdValorem { get; set; }

    /// <summary>
    /// Indicador detalle
    /// </summary>
    public bool? ProIndicadorDetalle { get; set; }

    /// <summary>
    /// Código descuento
    /// </summary>
    [StringLength(10)]
    [Unicode(false)]
    public string? ProCodigoDescuento { get; set; }

    /// <summary>
    /// Cargo por Distribucion
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProCargoDistribucion { get; set; }

    /// <summary>
    /// Codigo Sector  fk:Sectores
    /// </summary>
    [StringLength(10)]
    [Unicode(false)]
    public string? SecCodigo { get; set; }

    /// <summary>
    /// Usuario que creo o modifico el registro
    /// </summary>
    [StringLength(64)]
    [Unicode(false)]
    public string? UsuInicioSesion { get; set; }

    /// <summary>
    /// Fecha Creacion/Actualizacion 
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? ProFechaActualizacion { get; set; }

    /// <summary>
    /// Codigo Unidad de Medida fk:UsosMultiples Grupo:UnmCodigo
    /// </summary>
    [StringLength(15)]
    [Unicode(false)]
    public string? UnmCodigo { get; set; }

    /// <summary>
    /// Imagen producto
    /// </summary>
    [StringLength(500)]
    [Unicode(false)]
    public string? ProImg { get; set; }

    /// <summary>
    /// Identificador Unico
    /// </summary>
    [Column("rowguid")]
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Peso
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProPeso { get; set; }

    /// <summary>
    /// Volumen
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProVolumen { get; set; }

    [StringLength(2000)]
    [Unicode(false)]
    public string? ProGrupoProductos { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? ProLotes { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProHolgura { get; set; }

    [StringLength(5000)]
    [Unicode(false)]
    public string? ProInventarios { get; set; }

    [StringLength(5000)]
    [Unicode(false)]
    public string? ProListaPrecios { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProDescuentoMaximo { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProCantidadMinVenta { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProCantidadMaxVenta { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProCantidadMultiploVenta { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string? ProColor { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string? ProPaisOrigen { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string? ProAnio { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string? ProMedida { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? ProEstado { get; set; }

    public Guid? ProAtributo1 { get; set; }

    public Guid? ProAtributo2 { get; set; }
}
