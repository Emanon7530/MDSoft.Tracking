using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

[PrimaryKey("LotSecadoMaquina", "LotPosicion")]
[Table("LotesSecadoMaquinaDetalle")]
public partial class LotesSecadoMaquinaDetalle
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string LotSecadoMaquina { get; set; } = null!;

    [Key]
    public int LotPosicion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }

    [ForeignKey("LotSecadoMaquina")]
    [InverseProperty("LotesSecadoMaquinaDetalles")]
    public virtual LotesSecadoMaquina LotSecadoMaquinaNavigation { get; set; } = null!;
}
