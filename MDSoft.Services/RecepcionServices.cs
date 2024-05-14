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
        IRepositorio<RecepcionesComprasDetalle> _RepoRecepcionDetalle;
        IRepositorio<ComprasProducto> _repoCompra;
        IMapper _mapper;

        public RecepcionServices(IMapper mapper)
        {
            _RepoRecepcion = new Repositorio<RecepcionesCompra>();
            _RepoRecepcionDetalle = new Repositorio<RecepcionesComprasDetalle>();
            _repoCompra = new Repositorio<ComprasProducto>();
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

        public async Task<IEnumerable<RecepcionesComprasDetalleDTO>> GetRecepcionesDetalle(int RecSecuencia)
        {
            try
            {
                var _param = new ParametrosDeQuery<RecepcionesComprasDetalle>(1, 100);

                _param.Where = x => x.RecSecuencia == RecSecuencia;

                var result = await _RepoRecepcionDetalle.EncontrarPor(_param);

                var entities = _mapper.Map<IEnumerable<RecepcionesComprasDetalleDTO>>(result);

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
                var result = await _RepoRecepcion.TraerTodos();

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
                var exist = await _RepoRecepcion.ObtenerPorId(recepcionCompra.RecSecuencia);

                var newRecord = _mapper.Map<RecepcionesCompra>(recepcionCompra);

                if (exist == null)
                {
                    var result = await _RepoRecepcion.Agregar(newRecord);
                }
                else
                {
                    var result = await _RepoRecepcion.Actualizar(newRecord);
                }

                return recepcionCompra;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<RecepcionesCompraDTO>> ExecuteGetComprasStoreProcedure(string comReferencia)
        {
            try
            {

                string storeProcedureName = "sp_GetRecepcionesCompras";
                string[] parameters = new string[] { "@ComReferencia" };
                string[] values = new string[] { comReferencia };

                RecepcionesCompra entidad = null;

                var storeResult = await _RepoRecepcion.ExecuteProcedureNonQuery(entidad, storeProcedureName, parameters, values);

                var newRecord = _mapper.Map<IEnumerable<RecepcionesCompraDTO>>(storeResult);

                return newRecord;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<RecepcionesCompraDTO> Actualizar(RecepcionesCompraDTO recepcionCompra)
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
        public async Task<RecepcionesComprasDetalleDTO> GuradarDetalle(RecepcionesComprasDetalleDTO recepcionDetalle)
        {
            try
            {

                // verificar si existe cabecera
                var rowExiste = await _RepoRecepcion.ObtenerPorId(recepcionDetalle.RecSecuencia, null);

                if (rowExiste == null)
                {
                    recepcionDetalle.recepcionesComprasDTO = await Guardar(recepcionDetalle.recepcionesComprasDTO);
                }

                var newRecord = _mapper.Map<RecepcionesComprasDetalle>(recepcionDetalle);

                var result = await _RepoRecepcionDetalle.Agregar(newRecord);

                var recepcionCompra = _mapper.Map<RecepcionesComprasDetalleDTO>(recepcionDetalle);

                return recepcionCompra;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
