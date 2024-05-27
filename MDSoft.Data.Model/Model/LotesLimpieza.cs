using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

[Table("LotesLimpieza")]
public partial class LotesLimpieza
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string LotLimpieza { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? LotFechaCreacion { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionCreacion { get; set; }

    [Column("LotPesoOriginalKG", TypeName = "decimal(13, 2)")]
    public decimal? LotPesoOriginalKg { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? LotPesoResultante { get; set; }

    public int? LotCantidadSacos { get; set; }

    [Column("LotPesosSacosKG", TypeName = "decimal(13, 2)")]
    public decimal? LotPesosSacosKg { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? LotPesoUltSacoKg { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LotFechaCierre { get; set; }

    [Column("usuiniciosesionCierre")]
    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionCierre { get; set; }

    [InverseProperty("LotLimpiezaNavigation")]
    public virtual ICollection<LotesLimpiezaDetalle> LotesLimpiezaDetalles { get; set; } = new List<LotesLimpiezaDetalle>();
}
