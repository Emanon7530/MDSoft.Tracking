using AutoMapper;
using MDSoft.Data.Common;
using MDSoft.Data.Interface;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services.Common;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
using PNComm.Common.Enums;
using System;
using System.Collections.Generic;
using System.Data;
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
        IRepositorio<ComprasProductosDetalle> _repoCompraDet;
        IRepositorio<LotesFermentacion> _repoFermentacion;
        IRepositorio<LotesSecadoNatural> _repoSecadoNatural;

        IRepositorio<LotesFermentacionDetalle> _repoFermentacionDet;
        IRepositorio<LotesSecadoNaturalDetalle> _repoSecadoNaturalDet;

        IMapper _mapper;

        public RecepcionServices(IMapper mapper)
        {
            _RepoRecepcion = new Repositorio<RecepcionesCompra>();
            _RepoRecepcionDetalle = new Repositorio<RecepcionesComprasDetalle>();
            _repoCompra = new Repositorio<ComprasProducto>();
            _repoCompraDet = new Repositorio<ComprasProductosDetalle>();

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
                    _param.Where = x => x.ComReferencia.Contains(Filter.ComReferencia);
                }

                //if (Filter.IdRepresentante != null)
                //{
                //    _param.Where = x => x..Contains(Filter.IdRepresentante);
                //}


                var result = await _RepoRecepcion.EncontrarPor(_param);

                var entities = _mapper.Map<IEnumerable<RecepcionesCompraDTO>>(result);

                return entities;
            }
            catch (Exception e)
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
            catch (Exception e)
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
            catch (Exception e)
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
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<RecepcionesCompraDTO> Cerrar(RecepcionesCompraDTO recepcionCompra, EstatusCompraProductos comEstatus)
        {
            try
            {
                //*** Cerrar Compra //
                var compra = await _repoCompra.ObtenerPorId(recepcionCompra.ComSecuencia);

                if (compra == null)
                {
                    throw new ApplicationException("No existe compra con esta secuencia!");
                }

                compra.ComEstatus = (short?)comEstatus;  //Recibido Completo
                compra.ComFechaActualizacion = DateTime.Now;

                await _repoCompra.Actualizar(compra);

                /// actualizar recepcion
                var newRecord = _mapper.Map<RecepcionesCompra>(recepcionCompra);
                newRecord.RecFechaActualizacion = DateTime.Now;
                var result = await _RepoRecepcion.Actualizar(newRecord);

                return recepcionCompra;
            }
            catch (Exception e)
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
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="recepcionDetalle"></param>
        /// <returns></returns>
        public async Task<RecepcionesComprasDetalleDTO> GuradarDetalle(RecepcionesComprasDetalleDTO recepcionDetalle)
        {
            try
            {

                // Actualizar el producto de la compra recibido
                var _param = new ParametrosDeQuery<ComprasProductosDetalle>(1, 100)
                {
                    Where = x => x.ComReferencia == recepcionDetalle.recepcionesComprasDTO.ComReferencia && x.ComPosicion == recepcionDetalle.RecPosicion
                };

                var compradet = _repoCompraDet.EncontrarPor(_param).Result.First();

                if (compradet == null)
                {
                    throw new ApplicationException("No existe este producto en la compra!");
                }

                compradet.ComEstatusDetalle = 1; // Recibido;
                compradet.ComFechaActualizacion = DateTime.Now;
                await _repoCompraDet.Actualizar(compradet);

                // Actualizar Lote 
                if (recepcionDetalle.RecDestino == 2) // Fermentacion
                {
                    _repoFermentacionDet = new Repositorio<LotesFermentacionDetalle>();
                    var lote = new LotesFermentacionDetalle()
                    {
                        LotFermentacion = recepcionDetalle.LotReferencia,
                        ComReferencia = recepcionDetalle.ComReferencia,
                        LotFermentacionSecuencia = 1

                    };

                    await _repoFermentacionDet.Agregar(lote);
                }
                else
                {
                    _repoSecadoNaturalDet = new Repositorio<LotesSecadoNaturalDetalle>();
                    var lote = new LotesSecadoNaturalDetalle()
                    {
                        LotSecadoManual = recepcionDetalle.LotReferencia,
                        ComReferencia = recepcionDetalle.ComReferencia,
                        LotPosicion = await _repoSecadoNaturalDet.Contar(x => x.LotSecadoManual == recepcionDetalle.LotReferencia) + 1
                    };

                    await _repoSecadoNaturalDet.Agregar(lote);
                };

                // Insertar detalle recepcion 
                //// verificar si existe cabecera
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
