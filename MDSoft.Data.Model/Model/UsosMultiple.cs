using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

/// <summary>
/// Maestra de Usos Multiples
/// </summary>
[PrimaryKey("CodigoGrupo", "CodigoUso")]
public partial class UsosMultiple
{
    /// <summary>
    /// Código grupo
    /// </summary>
    [Key]
    [StringLength(25)]
    [Unicode(false)]
    public string CodigoGrupo { get; set; } = null!;

    /// <summary>
    /// Código uso
    /// </summary>
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string CodigoUso { get; set; } = null!;

    /// <summary>
    /// Descripción
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    /// <summary>
    /// Usuario que creo o modifico el registro
    /// </summary>
    [StringLength(64)]
    [Unicode(false)]
    public string? UsuInicioSesion { get; set; }

    /// <summary>
    /// Fecha Creacion/Actualizacion 
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? UsoFechaActualizacion { get; set; }

    /// <summary>
    /// Identificador Unico
    /// </summary>
    [Column("rowguid")]
    public Guid Rowguid { get; set; }
}
