using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MDSoft.Data.Interface;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Model.Model;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
namespace MDSoft.Tracking.Services
{
    public class LoteSecadoNaturalServices 
    {
        IRepositorio<LotesSecadoNatural> _Repolote;
        IMapper _mapper;

        public LoteSecadoNaturalServices(IMapper mapper )
        {

            _Repolote = new Repositorio<LotesSecadoNatural>();
            _mapper = mapper;
        }
        public async Task<IEnumerable<LotesSecadoNaturalDTO>> GetAll()
        {

            List<LotesSecadoNaturalDTO> dtoresult = new List<LotesSecadoNaturalDTO>();

            var result = await _Repolote.TraerTodos();

            _mapper.Map(result, dtoresult);

            return dtoresult;
        }

        public async Task<LotesSecadoNaturalDTO> GetById(int LoteNumber)
        {

            LotesSecadoNaturalDTO dtoresult = new LotesSecadoNaturalDTO();

            var result = await _Repolote.ObtenerPorId(LoteNumber);

            _mapper.Map(result, dtoresult);

            return dtoresult;
        }

        public async Task<LotesSecadoNaturalDTO> Guardar(LotesSecadoNaturalDTO loteFermentacion)
        {

            LotesSecadoNatural result = new LotesSecadoNatural();

            result = _mapper.Map<LotesSecadoNatural>(loteFermentacion);

            await _Repolote.Agregar(result);

            loteFermentacion = _mapper.Map<LotesSecadoNaturalDTO>(result);

            return loteFermentacion;
        }
    }
}
