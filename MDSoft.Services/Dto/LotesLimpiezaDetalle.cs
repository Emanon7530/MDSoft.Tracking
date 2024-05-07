using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Services.DTO;

public partial class LotesLimpiezaDetalleDTO
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string LotLimpieza { get; set; } = null!;

    [Key]
    public int LotPosicion { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? LotSecadoArtificial { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }
    public string? NombreComReferencia { get; set; }

    public virtual LotesLimpiezaDTO LotLimpiezaDTO { get; set; } = null!;
}
