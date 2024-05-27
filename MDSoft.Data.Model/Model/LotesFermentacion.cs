using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

[Table("LotesFermentacion")]
public partial class LotesFermentacion
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string LotFermentacion { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? LotFechaCreacion { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? LotUsuarioCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LotFechaCierre { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? LotUsuarioCierre { get; set; }

    [InverseProperty("LotFermentacionNavigation")]
    public virtual ICollection<LotesFermentacionDetalle> LotesFermentacionDetalles { get; set; } = new List<LotesFermentacionDetalle>();
}
