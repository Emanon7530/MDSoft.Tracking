using AutoMapper;
using MDSoft.Tracking.Services;
using MDSoft.Tracking.Services.Common;
using MDSoft.Tracking.Services.Dto;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using System.Linq.Expressions;

namespace MDSoft.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComprasProductosController : ControllerBase
    {
        private readonly ILogger<ComprasProductosController> _logger;

        private readonly LoteFermentacionServices _loteFermentacionServices;
        private readonly ComprasProductosSevices _compraServices;

        IMapper _mapper;

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
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet()]
        [Route("GetCompraByTicket")]
        public async Task<ComprasProductoDTO> GetCompraByTicket(string ticketNumber)
        {
            try
            {
                var result = await _compraServices.GetCompraByTicket(ticketNumber);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet()]
        [Route("GetProductInCompraByCode")]
        public async Task<ComprasProductosDetalleDTO> GetProductInCompraByCode(ComprasProductoDTO compra,int productId)
        {
            try
            {
                var result = await _compraServices.GetProductInCompraByCode(compra, productId);

                return result;
            }
            catch (Exception)
            {

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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
