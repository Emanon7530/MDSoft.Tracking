using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services.DTO;

namespace Tracking.Services
{
    public interface IAPIManager
    {
        //HttpClient Client { get; }

        Task<bool> LoteFermentacionExists(string loteFermentacion);
        Task<bool> LoteSecadoNaturalExists(string loteSecadoNatural);

        Task<RecepcionesProductoDTO> ActualizarRecepcion(RecepcionesProductoDTO recepcionDetalle);
        Task<ComprasProductoDTO> GetCompraByTicket(string repCodigo, int comSecuencia);
        Task<List<ComprasProductoDTO>> GetComprasPendientes(DateTime fromDate, DateTime toDate);
        Task<ComprasProductosDetalleDTO> GetProductInCompraByCode(string repCodigo, int comSecuencia, int proID);
        Task<ComprasProductosDetalleDTO> GetProductInCompraByReference(string repCodigo, int comSecuencia, string comReference);
        Task<IEnumerable<RecepcionesProductosDetalleDTO>> GetRecepcionesDetalle(int recSecuencia);
        Task<LotesFermentacionDTO> GuardarLoteFermentacion(LotesFermentacionDTO loteFermentacion);
        Task<LotesSecadoNaturalDTO> GuardarLoteSecadoNatural(LotesSecadoNaturalDTO loteFermentacion);
        Task<RecepcionesProductoDTO> GuardarRecepcion(RecepcionesProductoDTO recepcionDetalle);
        Task<RecepcionesProductosDetalleDTO> GuradarDetalleRecepcion(RecepcionesProductosDetalleDTO recepcionDetalle);
        Task<IEnumerable<ComprasProductoDTO>> sp_GetComprasPendientes();
        Task<IEnumerable<ComprasProductoDTO>> sp_GetComprasHistoricoPendientes();

    }
}