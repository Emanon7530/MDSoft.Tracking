using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Services.Dto;

/// <summary>
/// Detalle de Compras
/// </summary>
public partial class ComprasDetalleDTO
{
    /// <summary>
    /// Codigo de Representante
    /// </summary>
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string RepCodigo { get; set; } = null!;

    /// <summary>
    /// Secuencia
    /// </summary>
    [Key]
    public int ComSecuencia { get; set; }

    /// <summary>
    /// Posición
    /// </summary>
    [Key]
    public int ComPosicion { get; set; }

    /// <summary>
    /// ID Producto
    /// </summary>
    [Column("ProID")]
    public int? ProId { get; set; }

    /// <summary>
    /// Cantidad
    /// </summary>
    public int? ComCantidad { get; set; }

    /// <summary>
    /// Cantidad detalle
    /// </summary>
    public int? ComCantidadDetalle { get; set; }

    /// <summary>
    /// Precio
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComPrecio { get; set; }

    /// <summary>
    /// Itbis
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComItbis { get; set; }

    /// <summary>
    /// Selectivo
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComSelectivo { get; set; }

    /// <summary>
    /// Impuesto AdValorem
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComAdValorem { get; set; }

    /// <summary>
    /// Descuento
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComDescuento { get; set; }

    /// <summary>
    /// Total Itbis
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComTotalItbis { get; set; }

    /// <summary>
    /// Total descuento
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComTotalDescuento { get; set; }

    /// <summary>
    /// Indicador oferta
    /// </summary>
    public bool? ComindicadorOferta { get; set; }

    /// <summary>
    /// Número de documento cuentas por cobrar
    /// </summary>
    [Column("cxcDocumento")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CxcDocumento { get; set; }

    /// <summary>
    /// Usuario que creo o modifico el registro
    /// </summary>
    [StringLength(64)]
    [Unicode(false)]
    public string? UsuInicioSesion { get; set; }
    public string? NombreUsuInicioSesion { get; set; }

    /// <summary>
    /// Fecha Creacion/Actualizacion 
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? ComFechaActualizacion { get; set; }

    /// <summary>
    /// Identificador Unico
    /// </summary>
    [Column("rowguid")]
    public Guid Rowguid { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? RepSupervisor { get; set; }
    public string? NombreRepSupervisor { get; set; }

    public int? ComCantidadAprobada { get; set; }
}
