using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Services.DTO;

public partial class LotesSecadoMaquinaDTO
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
    public string? NombreUsuiniciosesionCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LotFechaCierre { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionCierre { get; set; }
    public string? NombreUsuiniciosesionCierre { get; set; }

    public virtual ICollection<LotesSecadoMaquinaDetalleDTO> LotesSecadoMaquinaDetallesDTO { get; set; } = new List<LotesSecadoMaquinaDetalleDTO>();
}
