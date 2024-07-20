using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Services.DTO;

public partial class ComprasProductosDetalleDTO
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string RepCodigo { get; set; } = null!;

    [Key]
    public int ComSecuencia { get; set; }

    [Key]
    public int ComPosicion { get; set; }

    [Column("ProID")]
    public int? ProId { get; set; }
    public string ProDescripcion { get; set; } = null!;

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

    [StringLength(5)]
    [Unicode(false)]
    public int? ComEstadoProducto { get; set; }

    [Unicode(false)]
    public string? ComTipoCertificacion { get; set; }

    [Column(TypeName = "decimal(5, 3)")]
    public decimal? ComHumedad { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComPesoKg { get; set; }

    [Column("ComKGQuintal")]
    public short? ComKgquintal { get; set; }

    public string? ComTipoProducto { get; set; }

    [Column("ComBRIX")]
    public int? ComBrix { get; set; }

    [Column("unmCodigo")]
    [StringLength(5)]
    [Unicode(false)]
    public string? UnmCodigo { get; set; }

    [Column("comPesoQuintal", TypeName = "decimal(13, 2)")]
    public decimal? ComPesoQuintal { get; set; }

    [Column("comPrecioQuintal", TypeName = "decimal(13, 2)")]
    public decimal? ComPrecioQuintal { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComPesoBruto { get; set; }

    [Column("ComPrecioKG", TypeName = "decimal(13, 2)")]
    public decimal? ComPrecioKg { get; set; }

    public virtual ComprasProductoDTO ComprasProducto { get; set; } = null!;
}
