using AutoMapper;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services;
using MDSoft.Tracking.Services.Common;
using MDSoft.Tracking.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MDSoft.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecepcionesProductosController : ControllerBase
    {
        private readonly ILogger<RecepcionesProductosController> _logger;

        private readonly LoteFermentacionServices _loteFermentacionServices;
        private readonly RecepcionesProductosServices _recepcionServices;
        private readonly ComprasProductosSevices _compraServices;

        IMapper _mapper;

        public RecepcionesProductosController(ILogger<RecepcionesProductosController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;

            _loteFermentacionServices = new LoteFermentacionServices(_mapper);
            _recepcionServices = new RecepcionesProductosServices(_mapper);
            _compraServices = new ComprasProductosSevices(_mapper);

        }

        [HttpGet()]
        [Route("GetAll")]
        public async Task<IEnumerable<RecepcionesProductoDTO>> GetAll()
        {
            try
            {
                var result = await _recepcionServices.GetAll();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet()]
        [Route("ExecuteGetComprasStoreProcedure")]
        public async Task<IEnumerable<RecepcionesProductoDTO>> ExecuteGetComprasStoreProcedure(string comReferencia)
        {
            try
            {
                var result = await _recepcionServices.ExecuteGetComprasStoreProcedure(comReferencia);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet()]
        [Route("GetRecepciones")]
        public async Task<IEnumerable<RecepcionesProductoDTO>> GetRecepciones(RecepcionesProductoDTO Filter)
        {
            try
            {
                var result = await _recepcionServices.GetRecepciones(Filter);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost()]
        [Route("Guardar")]
        public async Task<bool> Guardar(RecepcionesProductoDTO recepcionCompra)
        {
            try
            {
                var result = await _recepcionServices.Guardar(recepcionCompra);

                return true;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPut()]
        [Route("Actualizar")]
        public async Task<RecepcionesProductoDTO> Actualizar(RecepcionesProductoDTO recepcionCompra)
        {
            try
            {
                var result = await _recepcionServices.Actualizar(recepcionCompra);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut()]
        [Route("Cerrar")]
        public async Task<RecepcionesProductoDTO> Cerrar(RecepcionesProductoDTO recepcionCompra)
        {
            try
            {
                var result = await _recepcionServices.Cerrar(recepcionCompra);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost()]
        [Route("GuardarLinear")]
        public async Task<IEnumerable<RecepcionesProductosDetalleDTO>> GuardarLinear(RecepcionesProductosDetalleDTO recepcionCompraDetalle)
        {
            try
            {
                var result = await _recepcionServices.GuradarLinear(recepcionCompraDetalle);

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost()]
        [Route("GuardarDetalle")]
        public async Task<RecepcionesProductosDetalleDTO> GuardarDetalle(RecepcionesProductosDetalleDTO recepcionCompraDetalle)
        {
            try
            {
                var result = await _recepcionServices.GuradarDetalle(recepcionCompraDetalle);

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
