using AutoMapper;
using MDSoft.Tracking.Services;
using MDSoft.Tracking.Services.Common;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using System.Linq.Expressions;

namespace MDSoft.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecepcionController : ControllerBase
    {
        private readonly ILogger<RecepcionController> _logger;

        private readonly LoteFermentacionServices _loteFermentacionServices;
        private readonly RecepcionServices _recepcionServices;
        private readonly ComprasProductosSevices _compraServices;

        IMapper _mapper;

        public RecepcionController(ILogger<RecepcionController> logger, IMapper mapper)
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
    }
}