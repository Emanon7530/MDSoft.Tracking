using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

[Keyless]
public partial class VwComprasProductosDetalleDTO
{
    [StringLength(15)]
    [Unicode(false)]
    public string RepCodigo { get; set; } = null!;

    public int ComSecuencia { get; set; }

    public int ComPosicion { get; set; }

    [Column("ProID")]
    public int? ProId { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComCantidad { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComPrecio { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComItbis { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComSelectivo { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComAdValorem { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComDescuento { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComTotalItbis { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComTotalDescuento { get; set; }

    [Column("comReferencia")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }

    public int? ComEstatusDetalle { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? RepSupervisor { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ComFechaActualizacion { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuInicioSesion { get; set; }

    [Column("rowguid")]
    public Guid Rowguid { get; set; }

    public string? ComTipoProducto { get; set; }

    [Unicode(false)]
    public string? ComTipoCertificacion { get; set; }

    [Column(TypeName = "decimal(5, 3)")]
    public decimal? ComHumedad { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComPesoKg { get; set; }

    [Column("ComKGQuintal")]
    public short? ComKgquintal { get; set; }

    [Column("ComBRIX")]
    public int? ComBrix { get; set; }

    [Column("ComNumeroERP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? ComNumeroErp { get; set; }
}
