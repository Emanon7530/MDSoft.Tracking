using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

[PrimaryKey("RecSecuencia", "RecPosicion")]
[Table("RecepcionesComprasDetalle")]
public partial class RecepcionesComprasDetalle
{
    [Key]
    public int RecSecuencia { get; set; }

    [Key]
    public int RecPosicion { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComPeso { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? RecPeso { get; set; }

    public int? RecDestino { get; set; }

    [ForeignKey("RecSecuencia")]
    [InverseProperty("RecepcionesComprasDetalles")]
    public virtual RecepcionesCompra RecSecuenciaNavigation { get; set; } = null!;
}
