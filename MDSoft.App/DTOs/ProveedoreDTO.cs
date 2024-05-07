using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tracking.DTOs;

public partial class ProveedoreDTO
{
    public string ProCodigo { get; set; } = null!;

    public string ProNombre { get; set; } = null!;

    public string? ProRnc { get; set; }

    public int? PaiId { get; set; }

    public int? ProId { get; set; }

    public int? MunId { get; set; }

    public string ProCalle { get; set; } = null!;

    public string ProCasa { get; set; } = null!;

    public string ProUrbanizacion { get; set; } = null!;

    public string ProTelefono { get; set; } = null!;

    public string ProContacto { get; set; } = null!;

    public decimal ProLimiteCredito { get; set; }

    public string? ProWhatsapp { get; set; }

    public bool? ProIndicadorExonerado { get; set; }

    public string? LiPcodigo { get; set; }

    public decimal? ProPromedioCompra { get; set; }

    public string? ProTipoComprobanteFac { get; set; }

    public string? ProTipoComprobanteNc { get; set; }

    public bool? ProIndicadorPresentacion { get; set; }

    public DateTime? ProFechaUltimaCompra { get; set; }

    public int? ProEstatus { get; set; }

    public string? ProSector { get; set; }

    public string? RepCodigo { get; set; }

    public string? MonCodigo { get; set; }

    public string? ProLicencia { get; set; }

    public decimal? ProLongitud { get; set; }

    public decimal? ProLatitud { get; set; }

    public string? ProEncargadoPago { get; set; }

    public bool? ProIndicadorDeposito { get; set; }

    public string? ProCorreoElectronico { get; set; }

    public string? ProPropietario { get; set; }

    public string? ProCedulaPropietario { get; set; }

    public string? ProPaginaWeb { get; set; }

    public string? ProFormasPago { get; set; }

    public DateTime? ProFechaCreacion { get; set; }
    public string? PrdDirTipo { get; set; }

    public int? TiNid { get; set; }

    public int? CanId { get; set; }

    public int? ZonId { get; set; }

    [Column("ClaID")]
    public int? ClaId { get; set; }

    public int? ConId { get; set; }

    [Column("RutEntregaID")]
    public int? RutEntregaId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProCaracteristicas { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProMontoUltimaCompra { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProCompraAnioActual { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProComprasAnioAnterior { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ProFechaUltimoPago { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProMontoUltimoPago { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string? ProNombreComercial { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProValorDescuentoPromedio { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProTasaDescuentoPromedio { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? ProRegMercantil { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? ProDiaVisita { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? ProFrecuenciaVisita { get; set; }

    [StringLength(7)]
    [Unicode(false)]
    public string? ProRutSemana1 { get; set; }

    [StringLength(7)]
    [Unicode(false)]
    public string? ProRutSemana2 { get; set; }

    [StringLength(7)]
    [Unicode(false)]
    public string? ProRutSemana3 { get; set; }

    [StringLength(7)]
    [Unicode(false)]
    public string? ProRutSemana4 { get; set; }

    public int? ProOrdenRuta { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? ProFuente { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ProFechaActualizacion { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuInicioSesion { get; set; }

    [Column("rowguid")]
    public Guid? Rowguid { get; set; }
}
