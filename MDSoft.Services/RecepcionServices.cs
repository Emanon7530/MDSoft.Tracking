using AutoMapper;
using MDSoft.Data.Common;
using MDSoft.Data.Interface;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services.Common;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Services
{
    public class RecepcionServices
    {
        IRepositorio<RecepcionesCompra> _RepoRecepcion;
        IRepositorio<Compra> _repoCompra;
        IMapper _mapper;

        public RecepcionServices(IMapper mapper )
        {
            _RepoRecepcion = new Repositorio<RecepcionesCompra>();
            _repoCompra = new Repositorio<Compra>();
            _mapper = mapper;

        }
        public async Task<IEnumerable<RecepcionesCompraDTO>> GetRecepciones(RecepcionesComprasFilter Filter)
        {
            try
            {
                var _param = new ParametrosDeQuery<RecepcionesCompra>(1, 100);

                if (Filter == null)
                {
                    return null;
                }

                if (Filter.DateFrom != null)
                {
                    _param.Where = x => x.RecFechaCreacion >= Filter.DateFrom && x.RecFechaCreacion <= Filter.DateTo;
                }

                if (Filter.ComReferencia != null)
                {
                    _param.Where = x => x.Comreferencia.Contains(Filter.ComReferencia);
                }

                //if (Filter.IdRepresentante != null)
                //{
                //    _param.Where = x => x..Contains(Filter.IdRepresentante);
                //}


                var result = await _RepoRecepcion.EncontrarPor(_param);

                var entities = _mapper.Map<IEnumerable<RecepcionesCompraDTO>>(result);

                return entities;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<RecepcionesCompraDTO>> GetAll()
        {
            try
            {
                var _param = new ParametrosDeQuery<RecepcionesCompra>(1, 100);

                var result = await _RepoRecepcion.EncontrarPor(_param);

                var entities = _mapper.Map<IEnumerable<RecepcionesCompraDTO>>(result);

                return entities;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<RecepcionesCompraDTO> Guardar(RecepcionesCompraDTO recepcionCompra)
        {
            try
            {
                var newRecord = _mapper.Map<RecepcionesCompra>(recepcionCompra);

                var result = await _RepoRecepcion.Actualizar(newRecord);

                return recepcionCompra;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
