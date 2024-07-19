using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

[PrimaryKey("RepCodigo", "RecSecuencia", "RecPosicion")]
[Table("RecepcionesProductosDetalle")]
public partial class RecepcionesProductosDetalle
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string RepCodigo { get; set; } = null!;

    [Key]
    public int RecSecuencia { get; set; }

    [Key]
    public int RecPosicion { get; set; }

    [Column("ProID")]
    public int? ProId { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComPeso { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? RecPeso { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UsuInicioSesion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UsuFechaActualizacion { get; set; }

    public short? RecEstado { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecFechaActualizacion { get; set; }

    [Column("rowguid")]
    public Guid? Rowguid { get; set; }
}
