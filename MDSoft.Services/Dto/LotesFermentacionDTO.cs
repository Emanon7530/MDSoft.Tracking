using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Services.DTO;

public partial class LotesFermentacionDTO {
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string LotFermentacion { get; set; } = null!;

    [Column(TypeName = "datetime")]
     public DateTime? LotFechaCreacion { get; set; } 

    [StringLength(64)]
    [Unicode(false)]
    public string? LotUsuarioCreacion { get; set; }
    public string? NombreLotUsuarioCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LotFechaCierre { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? LotUsuarioCierre { get; set; }
    public string? NombreLotUsuarioCierre { get; set; }

    public virtual ICollection<LotesFermentacionDetalleDTO> LotesFermentacionDetallesDTO { get; set; } = new List<LotesFermentacionDetalleDTO>();
}
