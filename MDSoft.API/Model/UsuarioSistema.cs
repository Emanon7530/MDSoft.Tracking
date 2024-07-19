using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Model;

[Table("UsuarioSistema")]
[Index("Rowguid", Name = "IDXUsuarioSistemarowguid", IsUnique = true)]
public partial class UsuarioSistema
{
    [Key]
    [StringLength(64)]
    [Unicode(false)]
    public string UsuInicioSesion { get; set; } = null!;

    [StringLength(11)]
    [Unicode(false)]
    public string? UsuCedula { get; set; }

    [StringLength(32)]
    [Unicode(false)]
    public string? UsuClave { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? UsuNombres { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? UsuApellidos { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? UsuInstitucion { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? UsuDepartamento { get; set; }

    public bool? UsuEstatus { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? UsuCorreoElectronico { get; set; }

    [Column("CliID")]
    public int? CliId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UsuFechaActualizacion { get; set; }

    [Column("RolID")]
    public int? RolId { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? RepCodigo { get; set; }

    public bool? UsuFiltrarClientes { get; set; }

    [Column("rowguid")]
    public Guid Rowguid { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? DefaultUrl { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? CedCodigo { get; set; }

    public short? UsuIntentosClave { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UsuUltimoIntento { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? GrcCodigo { get; set; }
}
