using AutoMapper;
using MDSoft.Tracking.Services;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MDSoft.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComprasProductosController : ControllerBase
    {
        private readonly ILogger<ComprasProductosController> _logger;
        private readonly LoteFermentacionServices _loteFermentacionServices;
        private readonly ComprasProductosSevices _compraServices;
        private readonly IMapper _mapper;

        public ComprasProductosController(ILogger<ComprasProductosController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;

            _loteFermentacionServices = new LoteFermentacionServices(_mapper);
            _compraServices = new ComprasProductosSevices(_mapper);

        }

        [HttpGet()]
        [Route("GetAll")]
        public async Task<IEnumerable<ComprasProductoDTO>> GetAll()
        {
            try
            {
                var result = await _compraServices.GetAll();

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError("ComprasProductos_GetAll " + e.Message);
                throw;
            }
        }
        [HttpGet()]
        [Route("GetPendingByDate")]
        public async Task<IEnumerable<ComprasProductoDTO>> GetPendingByDate(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var result = await _compraServices.GetPendingByDate();

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError("ComprasProductos_GetPendingByDate " + e.Message);
                throw;
            }
        }

        [HttpGet()]
        [Route("sp_GetComprasPendientes")]
        public async Task<IEnumerable<ComprasProductoDTO>> sp_GetComprasPendientes()
        {
            try
            {
                var result = await _compraServices.sp_GetComprasPendientes();

                return result;
            }
            catch (Exception e)
            {

                _logger.LogError("ComprasProductos_sp_GetComprasPendientes " + e.Message);
                throw;
            }
        }


        [HttpGet()]
        [Route("GetCompraByTicket")]
        public async Task<ComprasProductoDTO> GetCompraByTicket(string repCodigo, int comSecuencia)
        {
            try
            {
                var result = await _compraServices.GetCompraByTicket(repCodigo, comSecuencia );

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError("ComprasProductos_GetCompraByTicket " + e.Message);
                throw;
            }
        }

        [HttpGet()]
        [Route("GetProductInCompraByCode")]
        public async Task<ComprasProductosDetalleDTO> GetProductInCompraByCode(string repCodigo, int comSecuencia, int productId)
        {
            try
            {
                var result = await _compraServices.GetProductInCompraByCode(repCodigo, comSecuencia, productId);

                return result;
            }
            catch (Exception e)
            {

                _logger.LogError("ComprasProductos_GetProductInCompraByCode " + e.Message);
                throw;
            }
        }

        [HttpGet()]
        [Route("GetProductInCompraByReference")]
        public async Task<ComprasProductosDetalleDTO> GetProductInCompraByReference(string repCodigo, int comSecuencia, string comReferencia)
        {
            try
            {
                var result = await _compraServices.GetProductInCompraByReference(repCodigo, comSecuencia, comReferencia);

                return result;
            }
            catch (Exception e)
            {

                _logger.LogError(e.TargetSite + " " + e.Message);
                throw;
            }
        }

        [HttpGet()]
        [Route("GetProductsInCompra")]
        public async Task<IEnumerable<ComprasProductosDetalleDTO>> GetProductsInCompra(string repCodigo, int comSecuencia )
        {
            try
            {
                var result = await _compraServices.GetProductsInCompra(repCodigo, comSecuencia);

                return result;
            }
            catch (Exception e)
            {

                _logger.LogError("ComprasProductos_GetProductsInCompra " + e.Message);
                throw;
            }
        }

        [HttpPost()]
        [Route("CerrarCompra")]
        public async Task<ComprasProductoDTO> CerrarCompra(ComprasProductoDTO compra)

        {
            try
            {
                var result = await _compraServices.CerrarCompra(compra );

                return result;
            }
            catch (Exception e)
            {

                _logger.LogError("ComprasProductos_CerrarCompra " + e.Message);
                throw;
            }
        }
    }
}
