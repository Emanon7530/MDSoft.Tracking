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
    public class LoteSecadoNaturalServices : IDisposable
    {
        IRepositorio<LotesSecadoNatural> _Repolote;
        IRepositorio<LotesSecadoNaturalDetalle> _RepoloteDet;
        IMapper _mapper;

        public LoteSecadoNaturalServices(IMapper mapper)
        {

            _Repolote = new Repositorio<LotesSecadoNatural>();
            _RepoloteDet = new Repositorio<LotesSecadoNaturalDetalle>();
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

        public async Task<LotesSecadoNaturalDetalleDTO> GuardarDetalle(LotesSecadoNaturalDetalleDTO lote)
        {

            LotesSecadoNaturalDetalle? result = new LotesSecadoNaturalDetalle();


            result = _RepoloteDet.EncontrarPor(new ParametrosDeQuery<LotesSecadoNaturalDetalle>(1, 100)
            {
                Where = x => x.LotSecadoManual == lote.LotSecadoManual && x.ComReferencia == lote.ComReferencia
            }).Result.FirstOrDefault();

            if (result == null)
            {

                lote.LotPosicion = await _RepoloteDet.Contar(x => x.LotSecadoManual == lote.LotSecadoManual) + 1;

                result = _mapper.Map<LotesSecadoNaturalDetalle>(lote);
                await _RepoloteDet.Agregar(result);
            }

            lote = _mapper.Map<LotesSecadoNaturalDetalleDTO>(result);

            return lote;
        }
        public async Task<LotesSecadoNaturalDTO> Guardar(LotesSecadoNaturalDTO loteFermentacion)
        {

            LotesSecadoNatural result = new LotesSecadoNatural();

            result = _mapper.Map<LotesSecadoNatural>(loteFermentacion);

            await _Repolote.Agregar(result);

            loteFermentacion = _mapper.Map<LotesSecadoNaturalDTO>(result);

            return loteFermentacion;
        }

        public async Task<bool> Existe(string numeroLote)
        {

            bool exists = false;

            exists = await _Repolote.Contar(x => x.LotSecadoManual.Equals(numeroLote)) > 0;

            return exists;
        }

        public void Dispose()
        {
            _Repolote = null;
            _RepoloteDet = null;
        }
    }
}
