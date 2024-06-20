using AutoMapper;
using MDSoft.Tracking.Services;
using MDSoft.Tracking.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MDSoft.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly ILogger<LotesController> _logger;

        IMapper _mapper;

        public LotesController(ILogger<LotesController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;

        }

        #region Lote Fermentacion
        [HttpGet()]
        [Route("GetFermentacionAll")]
        public async Task<IEnumerable<LotesFermentacionDTO>> GetFermentacionAll()
        {
            var loteferm = new LoteFermentacionServices(_mapper);

            var result = await loteferm.GetAll();

            return result;
        }
        [HttpGet()]
        [Route("GetFermentacionById")]
        public async Task<LotesFermentacionDTO> GetFermentacionById(int LoteNumber)
        {
            var loteferm = new LoteFermentacionServices(_mapper);

            var result = await loteferm.GetById(LoteNumber);

            return result;

        }

        [HttpPost()]
        [Route("GuardarLoteFermentacion")]
        public async Task<LotesFermentacionDTO> GuardarLoteFermentacion(LotesFermentacionDTO loteFermentacion)
        {
            var loteferm = new LoteFermentacionServices(_mapper);
            var result = await loteferm.Guardar(loteFermentacion);

            return result;
        }

        #endregion

        #region Lote Secado Narual
        [HttpGet()]
        [Route("GetSecadoNauralAll")]
        public async Task<IEnumerable<LotesSecadoNaturalDTO>> GetSecadoNauralAll()
        {
            var loteferm = new LoteSecadoNaturalServices(_mapper);

            var result = await loteferm.GetAll();

            return result;
        }
        [HttpGet()]
        [Route("GetSecadoNarualById")]
        public async Task<LotesSecadoNaturalDTO> GetSecadoNarualById(int LoteNumber)
        {
            var loteferm = new LoteSecadoNaturalServices(_mapper);

            var result = await loteferm.GetById(LoteNumber);

            return result;

        }

        [HttpPost()]
        [Route("GuardarLoteSecadoNatural")]
        public async Task<LotesSecadoNaturalDTO> GuardarLoteSecadoNatural(LotesSecadoNaturalDTO loteSecadoNatura)
        {
            var loteferm = new LoteSecadoNaturalServices(_mapper);
            var result = await loteferm.Guardar(loteSecadoNatura);

            return result;

        }

        #endregion
    }
}
