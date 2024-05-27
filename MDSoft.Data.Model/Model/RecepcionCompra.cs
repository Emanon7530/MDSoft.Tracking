using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

public partial class RecepcionCompra
{
    [Key]
    public int RecSecuencia { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime RecFecahCreacion { get; set; }

    [StringLength(50)]
    public string UsuInicioSesionCreacion { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime? RecFechaActualizacion { get; set; }

    [StringLength(50)]
    public string? UsuInicioSesionModifica { get; set; }

    [StringLength(50)]
    public string ComReferencia { get; set; } = null!;

    [InverseProperty("RecSecuencaNavigation")]
    public virtual ICollection<RecepcionComprasDetalle> RecepcionComprasDetalles { get; set; } = new List<RecepcionComprasDetalle>();
}
