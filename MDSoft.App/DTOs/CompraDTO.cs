
namespace Tracking.DTOs
{
    public class CompraDTO
    {
        public int Id { get; set; } 
        public int IdVendedor { get; set; }
        public string Vendedor { get; set; }
        public int IdRepresentante { get; set; }
        public string Representante { get; set; }
        public string CodigoCompra { get; set; }
        public decimal Total { get; set; }
        public decimal Peso { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public List<CompraDetalleDTO> compraDetalle { get; set; } = new List<CompraDetalleDTO>();
    }
}
