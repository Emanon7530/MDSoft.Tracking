using AutoMapper;
using MDSoft.Tracking.Services;
using MDSoft.Tracking.Services.Common;
using MDSoft.Tracking.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MDSoft.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecepcionComprasController : ControllerBase
    {
        private readonly ILogger<RecepcionComprasController> _logger;

        private readonly LoteFermentacionServices _loteFermentacionServices;
        private readonly RecepcionServices _recepcionServices;
        private readonly ComprasProductosSevices _compraServices;

        IMapper _mapper;

        public RecepcionComprasController(ILogger<RecepcionComprasController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;

            _loteFermentacionServices = new LoteFermentacionServices(_mapper);
            _recepcionServices = new RecepcionServices(_mapper);
            _compraServices = new ComprasProductosSevices(_mapper);

        }

        [HttpGet()]
        [Route("GetAll")]
        public async Task<IEnumerable<RecepcionesCompraDTO>> GetAll()
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
        public async Task<IEnumerable<RecepcionesCompraDTO>> ExecuteGetComprasStoreProcedure(string comReferencia)
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
        public async Task<IEnumerable<RecepcionesCompraDTO>> GetRecepciones(RecepcionesComprasFilter Filter)
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
        public async Task<RecepcionesCompraDTO> Guardar(RecepcionesCompraDTO recepcionCompra)
        {
            try
            {
                var result = await _recepcionServices.Guardar(recepcionCompra);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut()]
        [Route("Actualizar")]
        public async Task<RecepcionesCompraDTO> Actualizar(RecepcionesCompraDTO recepcionCompra)
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
        public async Task<RecepcionesCompraDTO> Cerrar(RecepcionesCompraDTO recepcionCompra)
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
        [Route("GuardarDetalle")]
        public async Task<RecepcionesComprasDetalleDTO> GuardarDetalle(RecepcionesComprasDetalleDTO recepcionCompraDetalle)
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
