using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Services.DTO;

public partial class LotesSecadoNaturalDetalleDTO
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
    public string? NombreComReferencia { get; set; }

    [JsonIgnore]
    public LotesSecadoNaturalDTO LotSecadoManualDTO { get; set; } = null!;
}
