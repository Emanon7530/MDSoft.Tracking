using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

[Table("LotesSecadoMaquina")]
public partial class LotesSecadoMaquina
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string LotSecadoMaquina { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? LotFechaCreacion { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LotFechaCierre { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionCierre { get; set; }
}
