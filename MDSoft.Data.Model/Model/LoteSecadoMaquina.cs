using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

[Table("LoteSecadoMaquina")]
public partial class LoteSecadoMaquina
{
    [Key]
    public int LotSecadoMaquina { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime LotFechaCreacion { get; set; }

    [StringLength(50)]
    public string LotUsuarioCreacion { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime? LotFechaCierre { get; set; }

    [StringLength(50)]
    public string? LotUsuarioCierre { get; set; }

    [InverseProperty("LotSecadoMaquinaNavigation")]
    public virtual ICollection<LotesSecadoMaquinaDetalle> LotesSecadoMaquinaDetalles { get; set; } = new List<LotesSecadoMaquinaDetalle>();
}
