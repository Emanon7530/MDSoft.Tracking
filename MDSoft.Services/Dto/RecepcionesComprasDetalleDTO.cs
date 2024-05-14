using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Services.DTO;

public partial class RecepcionesComprasDetalleDTO
{
    [Key]
    public int RecSecuencia { get; set; }

    [Key]
    public int RecPosicion { get; set; }

    public string? RepCodigo { get; set; }
    public DateTime? RecFechaCreacion { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }
    public string? NombreComReferencia { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComPeso { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? RecPeso { get; set; }

    [StringLength(5)]
    public string RecEstado { get; set; }

    public virtual RecepcionesCompraDTO recepcionesComprasDTO { get; set; } = null!;
}
