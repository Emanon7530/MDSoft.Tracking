using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MDSoft.Data.Interface;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
namespace MDSoft.Tracking.Services
{
    public class LoteSecadoMaquinaServices 
    {
        IRepositorio<LotesSecadoMaquina> _Repolote;
        IMapper _mapper;

        public LoteSecadoMaquinaServices(IMapper mapper )
        {
            _Repolote = new Repositorio<LotesSecadoMaquina>();
            _mapper=mapper;
        }
        public async Task<IEnumerable<LotesSecadoMaquinaDTO>> GetAll()
        {

            List<LotesSecadoMaquinaDTO> dtoresult = new List<LotesSecadoMaquinaDTO>();

            var result = await _Repolote.TraerTodos();

            _mapper.Map(result, dtoresult);

            return dtoresult;
        }

        public async Task<LotesSecadoMaquinaDTO> GetById(int LoteNumber)
        {

            LotesSecadoMaquinaDTO dtoresult = new LotesSecadoMaquinaDTO();

            var result = await _Repolote.ObtenerPorId(LoteNumber);

            _mapper.Map(result, dtoresult);

            return dtoresult;
        }

        public async Task<LotesSecadoMaquinaDTO> Guardar(LotesSecadoMaquinaDTO loteFermentacion)
        {

            LotesSecadoMaquina result = new LotesSecadoMaquina();

            result = _mapper.Map<LotesSecadoMaquina>(loteFermentacion);

            await _Repolote.Agregar(result);

            loteFermentacion = _mapper.Map<LotesSecadoMaquinaDTO>(result);

            return loteFermentacion;
        }
    }
}
