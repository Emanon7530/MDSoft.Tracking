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
    public class ProductosController : ControllerBase
    {
        private readonly ILogger<ProductosController> _logger;

        private readonly ProductoServices _productoServices;

        IMapper _mapper;

        public ProductosController(ILogger<ProductosController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;

            _productoServices = new ProductoServices(_mapper);

        }
        [HttpGet()]
        [Route("TestMapper")]
        public async Task<PadresDTO> TestMapper()
        {
            try
            {
                var test = new Padres();

                var testdto = _mapper.Map<PadresDTO>(test);

                return testdto;
            }
            catch (Exception)
            {

                throw;
            }
        }



        [HttpGet()]
        [Route("GetAll")]
        public async Task<IEnumerable<ProductoDTO>> GetAll()
        {
            try
            {
                var result = await _productoServices.GetAll();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet()]
        [Route("GetByCode")]
        public async Task<ProductoDTO> GetByCode(string productCode)
        {
            try
            {
                var result = await _productoServices.GetByCode(productCode);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet()]
        [Route("GetByBarCode")]
        public async Task<ProductoDTO> GetByBarCode(string barCode)
        {
            try
            {
                var result = await _productoServices.GetByBarCode(barCode);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
