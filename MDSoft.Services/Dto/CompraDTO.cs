using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Services.DTO;

/// <summary>
/// Maestra de Compras
/// </summary>
public partial class CompraDTO
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
    /// ID Cliente
    /// </summary>
    [Column("CliID")]
    public int CliId { get; set; }

    /// <summary>
    /// Fecha
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? ComFecha { get; set; }

    /// <summary>
    /// Estatus
    /// </summary>
    public short? ComEstatus { get; set; }
    public string Estatus { get; set; }

    /// <summary>
    /// Total
    /// </summary>
    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComTotal { get; set; }

    /// <summary>
    /// ID condición de pago
    /// </summary>
    [Column("ConID")]
    public int? ConId { get; set; }

    /// <summary>
    /// NCF
    /// </summary>
    [Column("ComNCF")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ComNcf { get; set; }

    /// <summary>
    /// Referencia
    /// </summary>
    [StringLength(20)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }

    /// <summary>
    /// Secuencia
    /// </summary>
    public int? CuaSecuencia { get; set; }

    /// <summary>
    /// Cantidad canastos
    /// </summary>
    public int? ComCantidadCanastos { get; set; }

    /// <summary>
    /// Secuencia
    /// </summary>
    public int? VisSecuencia { get; set; }

    /// <summary>
    /// Cédula
    /// </summary>
    [Column("CLDCedula")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Cldcedula { get; set; }

    /// <summary>
    /// Secuencia
    /// </summary>
    public int? DepSecuencia { get; set; }

    /// <summary>
    /// Cantidad detalle
    /// </summary>
    public int? ComCantidadDetalle { get; set; }

    /// <summary>
    /// Cantidad impresión
    /// </summary>
    public int? ComCantidadImpresion { get; set; }

    /// <summary>
    /// Usuario que creo o modifico el registro
    /// </summary>
    [StringLength(64)]
    [Unicode(false)]
    public string? UsuInicioSesion { get; set; }
    public string? NombreUsuarioInicioSesion { get; set; }

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

    /// <summary>
    /// Version de MovilBusiness
    /// </summary>
    [Column("mbVersion")]
    [StringLength(50)]
    [Unicode(false)]
    public string? MbVersion { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? RepSupervisor { get; set; }
    public string? NombreRepSupervisor { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? SecCodigo { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? ComTipoPago { get; set; }

    [Column("FopID")]
    public int? FopId { get; set; }

    public int? ComBonoDenomicacion { get; set; }

    public int? ComBonoCantidad { get; set; }

    public short? ComTipo { get; set; }
}
