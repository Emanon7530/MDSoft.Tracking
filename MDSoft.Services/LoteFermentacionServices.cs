using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
    public class LoteFermentacionServices : IDisposable
    {
        IRepositorio<LotesFermentacion> _Repolote;
        IRepositorio<LotesFermentacionDetalle> _RepoloteDet;

        IMapper _mapper;

        public LoteFermentacionServices(IMapper mapper)
        {
            _Repolote = new Repositorio<LotesFermentacion>();
            _RepoloteDet = new Repositorio<LotesFermentacionDetalle>();
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

        public async Task<LotesFermentacionDetalleDTO> GuardarDetalle(LotesFermentacionDetalleDTO loteFermentacion)
        {

            LotesFermentacionDetalle? result = new LotesFermentacionDetalle();


            result = _RepoloteDet.EncontrarPor(new ParametrosDeQuery<LotesFermentacionDetalle>(1, 100)
            {
                Where = x => x.LotFermentacion == loteFermentacion.LotFermentacion && x.ComReferencia == loteFermentacion.ComReferencia
            }).Result.FirstOrDefault();

            if (result == null)
            {
                loteFermentacion.LotFermentacionSecuencia = await _RepoloteDet.Contar(x => x.LotFermentacion == loteFermentacion.LotFermentacion) + 1;

                result = _mapper.Map<LotesFermentacionDetalle>(loteFermentacion);
                await _RepoloteDet.Agregar(result);
            }

            loteFermentacion = _mapper.Map<LotesFermentacionDetalleDTO>(result);

            return loteFermentacion;
        }

        public async Task<bool> Existe(string numeroLote)
        {

            bool exists = false;

            exists = await _Repolote.Contar(x => x.LotFermentacion.Equals(numeroLote)) > 0;

            return exists;
        }

        public void Dispose()
        {
            _Repolote = null;
            _RepoloteDet = null;
        }
    }
}
