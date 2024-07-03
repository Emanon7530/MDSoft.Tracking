using MDSoft.Tracking.Services.DTO;

namespace Tracking.Services
{
    public interface IAPIManager
    {
        HttpClient Client { get; }

        Task<RecepcionesCompraDTO> ActualizarRecepcion(RecepcionesCompraDTO recepcionDetalle);
        Task<ComprasProductoDTO> GetCompraByTicket(string ComReferencia);
        Task<List<ComprasProductoDTO>> GetComprasPendientes(DateTime fromDate, DateTime toDate);
        Task<ComprasProductosDetalleDTO> GetProductInCompraByCode(string repCodigo, int comSecuencia, int proID);
        Task<ComprasProductosDetalleDTO> GetProductInCompraByReference(string repCodigo, int comSecuencia, string comReference);
        Task<IEnumerable<RecepcionesComprasDetalleDTO>> GetRecepcionesDetalle(int recSecuencia);
        Task<LotesFermentacionDTO> GuardarLoteFermentacion(LotesFermentacionDTO loteFermentacion);
        Task<LotesSecadoNaturalDTO> GuardarLoteSecadoNatural(LotesSecadoNaturalDTO loteFermentacion);
        Task<RecepcionesCompraDTO> GuardarRecepcion(RecepcionesCompraDTO recepcionDetalle);
        Task<RecepcionesComprasDetalleDTO> GuradarDetalleRecepcion(RecepcionesComprasDetalleDTO recepcionDetalle);
        Task<IEnumerable<ComprasProductoDTO>> sp_GetComprasPendientes();
    }
}