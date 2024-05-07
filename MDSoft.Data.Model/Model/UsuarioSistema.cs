using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking;

/// <summary>
/// Maestra de Usuario del Sistema
/// </summary>
[Table("UsuarioSistema")]
[Index("Rowguid", Name = "IDXUsuarioSistemarowguid", IsUnique = true)]
public partial class UsuarioSistema
{
    /// <summary>
    /// Usuario que creo o modifico el registro
    /// </summary>
    [Key]
    [StringLength(64)]
    [Unicode(false)]
    public string UsuInicioSesion { get; set; } = null!;

    /// <summary>
    /// Cédula usuario
    /// </summary>
    [StringLength(11)]
    [Unicode(false)]
    public string? UsuCedula { get; set; }

    /// <summary>
    /// Clave
    /// </summary>
    [StringLength(72)]
    [Unicode(false)]
    public string? UsuClave { get; set; }

    /// <summary>
    /// Nombres usuario
    /// </summary>
    [StringLength(150)]
    [Unicode(false)]
    public string? UsuNombres { get; set; }

    /// <summary>
    /// Apellidos usuario
    /// </summary>
    [StringLength(150)]
    [Unicode(false)]
    public string? UsuApellidos { get; set; }

    /// <summary>
    /// Institución
    /// </summary>
    [StringLength(150)]
    [Unicode(false)]
    public string? UsuInstitucion { get; set; }

    /// <summary>
    /// Departamento
    /// </summary>
    [StringLength(150)]
    [Unicode(false)]
    public string? UsuDepartamento { get; set; }

    /// <summary>
    /// Estatus
    /// </summary>
    public bool? UsuEstatus { get; set; }

    /// <summary>
    /// Correo electrónico usuario
    /// </summary>
    [StringLength(250)]
    [Unicode(false)]
    public string? UsuCorreoElectronico { get; set; }

    /// <summary>
    /// ID Cliente
    /// </summary>
    [Column("CliID")]
    public int? CliId { get; set; }

    /// <summary>
    /// Fecha Creacion/Actualizacion 
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? UsuFechaActualizacion { get; set; }

    /// <summary>
    /// ID Rol
    /// </summary>
    [Column("RolID")]
    public int? RolId { get; set; }

    /// <summary>
    /// Codigo de Representante
    /// </summary>
    [StringLength(15)]
    [Unicode(false)]
    public string? RepCodigo { get; set; }

    /// <summary>
    /// Filtrar clientes
    /// </summary>
    public bool? UsuFiltrarClientes { get; set; }

    /// <summary>
    /// Identificador Unico
    /// </summary>
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
