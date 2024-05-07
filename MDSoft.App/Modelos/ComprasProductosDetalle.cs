using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace Tracking.Modelos;


[PrimaryKey("RepCodigo", "ComSecuencia", "ComPosicion")]
[Table("ComprasProductosDetalle")]
public partial class ComprasProductosDetalle
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

    [ForeignKey("ComSecuencia, RepCodigo")]
    [InverseProperty("ComprasProductosDetalles")]
    public virtual ComprasProducto ComprasProducto { get; set; } = null!;
}
