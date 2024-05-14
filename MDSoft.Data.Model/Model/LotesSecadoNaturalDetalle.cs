using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking;

[PrimaryKey("LotSecadoManual", "LotPosicion")]
[Table("LotesSecadoNaturalDetalle")]
public partial class LotesSecadoNaturalDetalle
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string LotSecadoManual { get; set; } = null!;

    [Key]
    public int LotPosicion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }

    [JsonIgnore]
    [ForeignKey("LotSecadoManual")]
    [InverseProperty("LotesSecadoNaturalDetalles")]
    public virtual LotesSecadoNatural LotSecadoManualNavigation { get; set; } = null!;
}
