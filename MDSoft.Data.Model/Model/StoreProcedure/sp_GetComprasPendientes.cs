using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model.StoreProcedure;

[PrimaryKey("ComSecuencia", "RepCodigo")]
public partial class sp_GetComprasPendientes
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string RepCodigo { get; set; } = null!;

    [Key]
    public int ComSecuencia { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ComFecha { get; set; }

    public short? ComEstatus { get; set; }

    public int? ComCantidadDetalle { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? RepNombre { get; set; }
}
