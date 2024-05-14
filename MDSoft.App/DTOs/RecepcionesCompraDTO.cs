﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tracking.DTOs;

public partial class RecepcionesCompraDTO
{
    [Key]
    public int RecSecuencia { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecFechaCreacion { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecFechaActualizacion { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionModificacion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Comreferencia { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? RecEstado { get; set; }

    public virtual ICollection<RecepcionesComprasDetalleDTO> RecepcionesComprasDetalles { get; set; } = new List<RecepcionesComprasDetalleDTO>();
}