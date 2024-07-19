using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Services.DTO;

[PrimaryKey("RecSecuencia", "RepCodigo")]
public partial class RecepcionesProductoDTO
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string RepCodigo { get; set; } = null!;

    [Key]
    public int RecSecuencia { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecFecha { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionCreacion { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionModificacion { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string RecReferencia { get; set; } = null!;

    public int? RecEstado { get; set; }
    public int? ComEstadoCompra { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecFechaActualizacion { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? RecNumeroFactura { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Comreferencia { get; set; }

    [Column("rowguid")]
    public Guid Rowguid { get; set; }
}
