using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

[PrimaryKey("LotFermentacion", "LotFermentacionSecuencia")]
[Table("LotesFermentacionDetalle")]
public partial class LotesFermentacionDetalle
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string LotFermentacion { get; set; } = null!;

    [Key]
    public int LotFermentacionSecuencia { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }
}
