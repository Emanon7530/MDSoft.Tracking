using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

[PrimaryKey("RepCodigo", "RecSecuencia", "RefSecuencia")]
[Table("RecepcionesComprasDetalle")]
[Index("Rowguid", Name = "IDXRecepcionesComprasDetallerowguid", IsUnique = true)]
public partial class RecepcionesComprasDetalle
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string RepCodigo { get; set; } = null!;

    [Key]
    public int RecSecuencia { get; set; }

    [Key]
    public int RefSecuencia { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecFechaRecogida { get; set; }

    [StringLength(18)]
    [Unicode(false)]
    public string? ProCodigo { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? ProDescripcion { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? RecCantidadOrdenada { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? RecCantidadRecibida { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? ProCodigoBarra { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UsuInicioSesion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UsuFechaActualizacion { get; set; }

    public short? RecEstado { get; set; }

    [Column("rowguid")]
    public Guid? Rowguid { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? AlmCodigo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecFechaActualizacion { get; set; }

    public int? RecPosicion { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComPeso { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? RecPeso { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? RecDestino { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? RecLote { get; set; }
}
