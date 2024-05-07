using AutoMapper;
using MDSoft.Tracking.Services;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace MDSoft.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoteFermentacionController : ControllerBase
    {
        private readonly ILogger<LoteFermentacionController> _logger;

        private readonly LoteFermentacionServices _loteFermentacionServices;
        IMapper _mapper;

        public LoteFermentacionController(ILogger<LoteFermentacionController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;

            _loteFermentacionServices = new LoteFermentacionServices(_mapper);

        }

        [HttpGet(Name = "GetAll")]
        public async Task<IEnumerable<LotesFermentacionDTO>> Get()
        {
            return await _loteFermentacionServices.GetAll();
        }

        [HttpGet("{LoteNumber}")]
        public async Task<LotesFermentacionDTO> GetById(int LoteNumber)
        {
            return await _loteFermentacionServices.GetById(LoteNumber);
        }
    }
}
