
namespace MDSoft.Tracking.Services.Repository
{
    public class ComprasRepresentante
    {
        public string RepCodigo { get; set; }
        public string ProCodigo { get; set; }
        public int ComSecuencia { get; set; }
        public DateTime? ComFecha { get; set; }
        public string RepNombre { get; set; }
        public string ProNombre { get; set; }
        public string ComReferencia { get; set; }
        public decimal? ComCantidadDetalle { get; set; }
        public int? ComEstatus { get; set; }
    }
}