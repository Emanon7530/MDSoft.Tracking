using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Services.DTO;

public partial class RecepcionesCompraDTO
{
    [Key]
    public int RecSecuencia { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecFechaCreacion { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionCreacion { get; set; }
    public string? NombreUsuiniciosesionCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecFechaActualizacion { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionModificacion { get; set; }
    public string? NombreUsuiniciosesionModificacion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }
    public int ComSecuencia { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? RecEstado { get; set; }

    public virtual ComprasProductoDTO compraProductoDTO { get; set; }

    public virtual ICollection<RecepcionesComprasDetalleDTO> RecepcionesComprasDetallesDTO { get; set; } = new List<RecepcionesComprasDetalleDTO>();
}
