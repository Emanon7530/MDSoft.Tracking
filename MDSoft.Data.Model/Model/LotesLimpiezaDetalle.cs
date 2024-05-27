using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

[PrimaryKey("LotLimpieza", "LotPosicion")]
[Table("LotesLimpiezaDetalle")]
public partial class LotesLimpiezaDetalle
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string LotLimpieza { get; set; } = null!;

    [Key]
    public int LotPosicion { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? LotSecadoMaquina { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }

    [ForeignKey("LotLimpieza")]
    [InverseProperty("LotesLimpiezaDetalles")]
    public virtual LotesLimpieza LotLimpiezaNavigation { get; set; } = null!;
}
