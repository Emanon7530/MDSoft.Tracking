using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace Tracking.Modelos;


/// <summary>
/// Maestra de Representantes 
/// </summary>
[Index("Rowguid", Name = "IDXRepresentantesrowguid", IsUnique = true)]
[Index("RepLicencia", Name = "IX_Representantes", IsUnique = true)]
[Index("RepSupervisor", Name = "IX_Representantes_1")]
public partial class Representante
{
    /// <summary>
    /// Codigo de Representante
    /// </summary>
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string RepCodigo { get; set; } = null!;

    /// <summary>
    /// Nombre
    /// </summary>
    [StringLength(25)]
    [Unicode(false)]
    public string RepNombre { get; set; }

    /// <summary>
    /// Cargo
    /// </summary>
    [StringLength(25)]
    [Unicode(false)]
    public string RepCargo { get; set; }

    /// <summary>
    /// Clasificación
    /// </summary>
    [StringLength(25)]
    [Unicode(false)]
    public string RepClasificacion { get; set; }

    /// <summary>
    /// Teléfono 1
    /// </summary>
    [StringLength(20)]
    [Unicode(false)]
    public string RepTelefono1 { get; set; }

    /// <summary>
    /// Teléfono 2
    /// </summary>
    [StringLength(20)]
    [Unicode(false)]
    public string RepTelefono2 { get; set; }

    /// <summary>
    /// Clave
    /// </summary>
    [StringLength(36)]
    [Unicode(false)]
    public string RepClave { get; set; }

    /// <summary>
    /// Indicador venta
    /// </summary>
    public bool? RepIndicadorVenta { get; set; }

    /// <summary>
    /// Inventario ventas
    /// </summary>
    public bool? RepInventarioVentas { get; set; }

    /// <summary>
    /// Pedidos
    /// </summary>
    public bool? RepPedidos { get; set; }

    /// <summary>
    /// Cobros
    /// </summary>
    public bool? RepCobros { get; set; }

    /// <summary>
    /// Devoluciones
    /// </summary>
    public bool? RepDevoluciones { get; set; }

    /// <summary>
    /// Entrega
    /// </summary>
    public bool? RepEntrega { get; set; }

    /// <summary>
    /// Depósito
    /// </summary>
    public bool? RepDeposito { get; set; }

    /// <summary>
    /// Investigación
    /// </summary>
    public bool? RepInvestigacion { get; set; }

    /// <summary>
    /// M1 Representantes
    /// </summary>
    public bool? RepM1 { get; set; }

    /// <summary>
    /// M2 Representantes
    /// </summary>
    public bool? RepM2 { get; set; }

    /// <summary>
    /// M3 Representantes
    /// </summary>
    public bool? RepM3 { get; set; }

    /// <summary>
    /// M4 Representantes
    /// </summary>
    public bool? RepM4 { get; set; }

    /// <summary>
    /// M5 Representantes
    /// </summary>
    public bool? RepM5 { get; set; }

    /// <summary>
    /// ID zona
    /// </summary>
    [Column("ZonID")]
    public int? ZonId { get; set; }

    /// <summary>
    /// Indicador supervisor
    /// </summary>
    public bool? RepIndicadorSupervisor { get; set; }

    /// <summary>
    /// Representante supervisor
    /// </summary>
    [StringLength(15)]
    [Unicode(false)]
    public string RepSupervisor { get; set; }

    /// <summary>
    /// División
    /// </summary>
    public int? RepDivision { get; set; }

    /// <summary>
    /// ID de almacén
    /// </summary>
    [Column("AlmID")]
    public int? AlmId { get; set; }

    /// <summary>
    /// Fecha última actualización
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? RepFechaUltimaActualizacion { get; set; }

    /// <summary>
    /// Clave Sync
    /// </summary>
    [StringLength(10)]
    [Unicode(false)]
    public string RepClaveSync { get; set; }

    /// <summary>
    /// Fecha indicador ruta visita
    /// </summary>
    public bool? RepIndicadorRutaVisitasFecha { get; set; }

    /// <summary>
    /// Licencia
    /// </summary>
    public Guid RepLicencia { get; set; }

    /// <summary>
    /// ID ruta
    /// </summary>
    [Column("RutID")]
    [StringLength(20)]
    [Unicode(false)]
    public string RutId { get; set; }

    /// <summary>
    /// ID equipo
    /// </summary>
    [Column("EquID")]
    [StringLength(15)]
    [Unicode(false)]
    public string EquId { get; set; }

    /// <summary>
    /// Fecha Creacion/Actualizacion 
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? RepFechaActualizacion { get; set; }

    /// <summary>
    /// Usuario que creo o modifico el registro
    /// </summary>
    [StringLength(64)]
    [Unicode(false)]
    public string UsuInicioSesion { get; set; }

    /// <summary>
    /// Identificador Unico
    /// </summary>
    [Column("rowguid")]
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Estatus
    /// </summary>
    public short? RepEstatus { get; set; }

    /// <summary>
    /// ID vehiculo
    /// </summary>
    [Column("VehID")]
    public int? VehId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string CedCodigo { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string RepColor { get; set; }
}
