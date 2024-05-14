using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model.StoreProcedure;

[PrimaryKey("RepCodigo", "ComSecuencia", "ComPosicion")]
public partial class sp_GetProductoEnCompra
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string RepCodigo { get; set; } = null!;

    [Key]
    public int ComSecuencia { get; set; }

    [Key]
    public int ComPosicion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ComFecha { get; set; }

    [Column("ProID")]
    public int? ProId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string ProDescripcion  { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string RepNombre  { get; set; } = null!;

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComCantidad { get; set; }

    [Column("comReferencia")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }


    [StringLength(5)]
    [Unicode(false)]
    public string? ComEstadoProducto { get; set; }

}
