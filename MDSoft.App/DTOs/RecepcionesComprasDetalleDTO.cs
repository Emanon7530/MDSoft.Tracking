using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tracking.DTOs;

public partial class RecepcionesComprasDetalleDTO
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

    public virtual RecepcionesCompraDTO RecSecuenciaNavigation { get; set; } = null!;
}
