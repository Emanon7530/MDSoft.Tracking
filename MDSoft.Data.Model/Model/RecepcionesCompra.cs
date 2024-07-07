using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

[PrimaryKey("RecSecuencia", "RepCodigo")]
[Index("Rowguid", Name = "IDXRecepcionesComprasrowguid", IsUnique = true)]
public partial class RecepcionesCompra
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string RepCodigo { get; set; } = null!;

    [Key]
    public int RecSecuencia { get; set; }

    [Column("LecID")]
    public int? LecId { get; set; }

    public int? RecEstado { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecFecha { get; set; }

    [Column("OrdID")]
    public int OrdId { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string RecReferencia { get; set; } = null!;

    [StringLength(18)]
    [Unicode(false)]
    public string? SupCodigo { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? SupNombre { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? SupCalle { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? SupTelefono { get; set; }

    [Column("SupRNC")]
    [StringLength(13)]
    [Unicode(false)]
    public string? SupRnc { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? SupProvincia { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RechoraInicial { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecHoraFinal { get; set; }

    [Column("RecEstatusERP")]
    [StringLength(1)]
    [Unicode(false)]
    public string? RecEstatusErp { get; set; }

    public bool? RecImpresa { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UsuFechaActualizacion { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? RecEstatusValidacion { get; set; }

    public short? RecCantidadBultos { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? RecMotivoObservacion { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? RecControlDrogas { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecFechaActualizacion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UsuInicioSesion { get; set; }

    [Column("rowguid")]
    public Guid Rowguid { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? AlmCodigo { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? RecNumeroFactura { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecFechaCreacion { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionCreacion { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuiniciosesionModificacion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Comreferencia { get; set; }
}
