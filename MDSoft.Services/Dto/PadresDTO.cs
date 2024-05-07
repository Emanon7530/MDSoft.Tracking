using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Services.Dto
{
    public class PadresDTO
    {
        [Column("codigo")]
        public int Id { get; set; }
        [Column("Nombrecompleto")]
        public string Nombre { get; set; }
        [Column("Fechanacimiento")]
        public DateTime FechaNacimiento { get; set; }
        public IEnumerable<HijosDTO> Hijos { get; set; }
    [StringLength(50)]
    [Unicode(false)]
    public string ProCodigo { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string ProNombre { get; set; } = null!;

    [Column("ProRNC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ProRnc { get; set; }

    [Column("PaiID")]
    public int? PaiId { get; set; }

    [Column("ProID")]
    public int? ProId { get; set; }

    [Column("MunID")]
    public int? MunId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string ProCalle { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ProCasa { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ProUrbanizacion { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ProTelefono { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ProContacto { get; set; } = null!;

    [Column(TypeName = "decimal(13, 2)")]
    public decimal ProLimiteCredito { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProWhatsapp { get; set; }

    public bool? ProIndicadorExonerado { get; set; }

    [Column("LiPCodigo")]
    [StringLength(10)]
    [Unicode(false)]
    public string? LiPcodigo { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProPromedioCompra { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? ProTipoComprobanteFac { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? ProTipoComprobanteNc { get; set; }

    public bool? ProIndicadorPresentacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ProFechaUltimaCompra { get; set; }

    public int? ProEstatus { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProSector { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? RepCodigo { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? MonCodigo { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProLicencia { get; set; }

    [Column(TypeName = "decimal(18, 15)")]
    public decimal? ProLongitud { get; set; }

    [Column(TypeName = "decimal(18, 15)")]
    public decimal? ProLatitud { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProEncargadoPago { get; set; }

    public bool? ProIndicadorDeposito { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProCorreoElectronico { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProPropietario { get; set; }

    [StringLength(13)]
    [Unicode(false)]
    public string? ProCedulaPropietario { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? ProPaginaWeb { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ProFormasPago { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ProFechaCreacion { get; set; }

    [Column("prdDirTipo")]
    [StringLength(15)]
    [Unicode(false)]
    public string? PrdDirTipo { get; set; }

    [Column("TiNID")]
    public int? TiNid { get; set; }

    [Column("CanID")]
    public int? CanId { get; set; }

    [Column("ZonID")]
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
    public class HijosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }

    public class Hijos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }



    public class Padres
    {
        public Padres()
        {
            Id = 1;
            Nombre = "Enriquillo";
            FechaNacimiento = DateTime.Now;
            Hijos = new List<Hijos>() {
                new Hijos() {
                Id=1,
                   Nombre="Eriana",
                   FechaNacimiento=DateTime.Now
            },
                new Hijos() {
                Id=1,
                   Nombre="Anerys",
                   FechaNacimiento=DateTime.Now
            }
            };
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public IEnumerable<Hijos> Hijos { get; set; }
    [StringLength(50)]
    [Unicode(false)]
    public string ProCodigo { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string ProNombre { get; set; } = null!;

    [Column("ProRNC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ProRnc { get; set; }

    [Column("PaiID")]
    public int? PaiId { get; set; }

    [Column("ProID")]
    public int? ProId { get; set; }

    [Column("MunID")]
    public int? MunId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string ProCalle { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ProCasa { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ProUrbanizacion { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ProTelefono { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ProContacto { get; set; } = null!;

    [Column(TypeName = "decimal(13, 2)")]
    public decimal ProLimiteCredito { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProWhatsapp { get; set; }

    public bool? ProIndicadorExonerado { get; set; }

    [Column("LiPCodigo")]
    [StringLength(10)]
    [Unicode(false)]
    public string? LiPcodigo { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? ProPromedioCompra { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? ProTipoComprobanteFac { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? ProTipoComprobanteNc { get; set; }

    public bool? ProIndicadorPresentacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ProFechaUltimaCompra { get; set; }

    public int? ProEstatus { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProSector { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? RepCodigo { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? MonCodigo { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProLicencia { get; set; }

    [Column(TypeName = "decimal(18, 15)")]
    public decimal? ProLongitud { get; set; }

    [Column(TypeName = "decimal(18, 15)")]
    public decimal? ProLatitud { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProEncargadoPago { get; set; }

    public bool? ProIndicadorDeposito { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProCorreoElectronico { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProPropietario { get; set; }

    [StringLength(13)]
    [Unicode(false)]
    public string? ProCedulaPropietario { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? ProPaginaWeb { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ProFormasPago { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ProFechaCreacion { get; set; }

    [Column("prdDirTipo")]
    [StringLength(15)]
    [Unicode(false)]
    public string? PrdDirTipo { get; set; }

    [Column("TiNID")]
    public int? TiNid { get; set; }

    [Column("CanID")]
    public int? CanId { get; set; }

    [Column("ZonID")]
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

}
