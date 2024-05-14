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
    public class LoteLimpiezaServices     
    {
        IRepositorio<LotesLimpieza> _Repolote;
        IMapper _mapper;

        public LoteLimpiezaServices(IMapper mapper )
        {
            _Repolote = new Repositorio<LotesLimpieza>();
            _mapper=mapper;
        }
        public async Task<IEnumerable<LotesLimpiezaDTO>> GetAll()
        {

            List<LotesLimpiezaDTO> dtoresult = new List<LotesLimpiezaDTO>();

            var result = await _Repolote.TraerTodos();

            _mapper.Map(result, dtoresult);

            return dtoresult;
        }

        public async Task<LotesLimpiezaDTO> GetById(int LoteNumber)
        {

            LotesLimpiezaDTO dtoresult = new LotesLimpiezaDTO();

            var result = await _Repolote.ObtenerPorId(LoteNumber);

            _mapper.Map(result, dtoresult);

            return dtoresult;
        }

        public async Task<LotesLimpiezaDTO> Guardar(LotesLimpiezaDTO loteFermentacion)
        {

            LotesLimpieza result = new LotesLimpieza();

            result = _mapper.Map<LotesLimpieza>(loteFermentacion);

            await _Repolote.Agregar(result);

            loteFermentacion = _mapper.Map<LotesLimpiezaDTO>(result);

            return loteFermentacion;
        }
    }
}
