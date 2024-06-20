using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MDSoft.Data.Common;
using MDSoft.Data.Interface;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Model.Model;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
namespace MDSoft.Tracking.Services
{
    public class LoteFermentacionServices     {
        IRepositorio<LotesFermentacion> _Repolote;

        IMapper _mapper;

        public LoteFermentacionServices(IMapper mapper)
        {
            _Repolote = new Repositorio<LotesFermentacion>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<LotesFermentacionDTO>> GetAll()
        {

            List<LotesFermentacionDTO> dtoresult = new List<LotesFermentacionDTO>();

            var result = await _Repolote.TraerTodos();

            _mapper.Map(result, dtoresult);

            return dtoresult;
        }

        public async Task<LotesFermentacionDTO> GetById(int LoteNumber)
        {

            LotesFermentacionDTO dtoresult = new LotesFermentacionDTO();

            var result = await _Repolote.ObtenerPorId(LoteNumber);

            _mapper.Map(result, dtoresult);

            return dtoresult;
        }

        public async Task<LotesFermentacionDTO> Guardar(LotesFermentacionDTO loteFermentacion)
        {

            LotesFermentacion result = new LotesFermentacion();

            result = _mapper.Map<LotesFermentacion>(loteFermentacion);

            await _Repolote.Agregar(result);

            loteFermentacion = _mapper.Map<LotesFermentacionDTO>(result);

            return loteFermentacion;
        }
    }
}
