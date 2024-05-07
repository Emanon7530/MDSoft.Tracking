using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Services.DTO;

public partial class LotesFermentacionDetalleDTO
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
    public string? NombreComReferencia { get; set; }

    public virtual LotesFermentacionDTO LotFermentacionDTO { get; set; } = null!;
}
