using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

public partial class RecepcionesCompra
{
    [Key]
    public int RecSecuencia { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecFechaCreacion { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecFechaActualizacion { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionModificacion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? RecEstado { get; set; }

    [InverseProperty("RecSecuenciaNavigation")]
    public virtual ICollection<RecepcionesComprasDetalle> RecepcionesComprasDetalles { get; set; } = new List<RecepcionesComprasDetalle>();
}
