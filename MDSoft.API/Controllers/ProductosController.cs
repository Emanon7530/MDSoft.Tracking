using AutoMapper;
using MDSoft.Tracking.Services;
using MDSoft.Tracking.Services.DTO;
using Microsoft.AspNetCore.Mvc;

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
