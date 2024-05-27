using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

[PrimaryKey("RecSecuenca", "RecPosicion")]
[Table("RecepcionComprasDetalle")]
public partial class RecepcionComprasDetalle
{
    [Key]
    public int RecSecuenca { get; set; }

    [Key]
    public int RecPosicion { get; set; }

    [StringLength(50)]
    public string ComReferencia { get; set; } = null!;

    [Column(TypeName = "decimal(12, 3)")]
    public decimal ComPeso { get; set; }

    [Column(TypeName = "decimal(12, 3)")]
    public decimal RecPeso { get; set; }

    [ForeignKey("RecSecuenca")]
    [InverseProperty("RecepcionComprasDetalles")]
    public virtual RecepcionCompra RecSecuencaNavigation { get; set; } = null!;
}
