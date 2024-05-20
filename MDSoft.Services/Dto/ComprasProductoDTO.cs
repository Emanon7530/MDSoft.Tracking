using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MDSoft.Tracking.Services.DTO;

public partial class ComprasProductoDTO
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string RepCodigo { get; set; } = null!;

    [Key]
    public int ComSecuencia { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ComFecha { get; set; }

    public short? ComEstatus { get; set; }
    public string ComEstatusNombre { get => ComEstatus == 2 ? "Pend." : "Recib"; }



    public string? RepNombre { get; set; }

    public int? ComCantidadDetalle { get; set; }

    [Column("ComNCF")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ComNcf { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ComReferencia { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComSubTotal { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComMontoItbis { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ComMontoTotal { get; set; }

    public int? ComCantidadImpresion { get; set; }

    [Column("proCodigo")]
    [StringLength(50)]
    [Unicode(false)]
    public string ProCodigo { get; set; } = null!;

    [Column("ConID")]
    public int? ConId { get; set; }

    public int? CuaSecuencia { get; set; }

    public int? VisSecuencia { get; set; }

    [Column("CLDCedula")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Cldcedula { get; set; }

    public int? DepSecuencia { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? RepSupervisor { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? UsuInicioSesion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ComFechaActualizacion { get; set; }

    [Column("rowguid")]
    public Guid Rowguid { get; set; }

    [Column("mbVersion")]
    [StringLength(50)]
    [Unicode(false)]
    public string? MbVersion { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? ComTipoPago { get; set; }

    public virtual ICollection<ComprasProductosDetalleDTO> ComprasProductosDetalles { get; set; } = new List<ComprasProductosDetalleDTO>();
}
