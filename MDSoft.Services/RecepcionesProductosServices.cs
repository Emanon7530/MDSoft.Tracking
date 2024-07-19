using AutoMapper;
using MDSoft.Data.Common;
using MDSoft.Data.Interface;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services.Common;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Enums;
using NuGet.Common;
using PNComm.Common.Enums;


namespace MDSoft.Tracking.Services
{
    public class RecepcionesProductosServices
    {
        IRepositorio<RecepcionesProducto> _RepoRecepcion;
        IRepositorio<RecepcionesProductosDetalle> _RepoRecepcionDetalle;
        IRepositorio<ComprasProducto> _repoCompra;
        IRepositorio<ComprasProductosDetalle> _repoCompraDet;

        LoteFermentacionServices _lotFermentacion;
        LoteSecadoNaturalServices _lotSecadoNatural;
        IMapper _mapper;

        public RecepcionesProductosServices(IMapper mapper)
        {
            _RepoRecepcion = new Repositorio<RecepcionesProducto>();
            _RepoRecepcionDetalle = new Repositorio<RecepcionesProductosDetalle>();
            _repoCompra = new Repositorio<ComprasProducto>();
            _repoCompraDet = new Repositorio<ComprasProductosDetalle>();

            //LoteFermentacionServices _lotFermentacion = new LoteFermentacionServices(mapper);
            //LoteSecadoNaturalServices _lotSecadoNatural = new LoteSecadoNaturalServices(mapper);

            _mapper = mapper;

        }
        public async Task<IEnumerable<RecepcionesProductoDTO>> GetRecepciones(RecepcionesProductoDTO Filter)
        {
            try
            {
                var _param = new ParametrosDeQuery<RecepcionesProducto>(1, 100);

                if (Filter == null)
                {
                    return null;
                }

                if (Filter.RecFecha != null)
                {
                    _param.Where = x => x.RecFecha >= Filter.RecFecha && x.RecFecha <= Filter.RecFecha;
                }

                if (Filter.Comreferencia != null)
                {
                    _param.Where = x => x.Comreferencia.Contains(Filter.Comreferencia);
                }

                var result = await _RepoRecepcion.EncontrarPor(_param);

                var entities = _mapper.Map<IEnumerable<RecepcionesProductoDTO>>(result);

                return entities;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public async Task<IEnumerable<RecepcionesProductoDTO>> GetAll()
        {
            try
            {
                var result = await _RepoRecepcion.TraerTodos();

                var entities = _mapper.Map<IEnumerable<RecepcionesProductoDTO>>(result);

                return entities;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<bool> Guardar(RecepcionesProductoDTO recepcionCompra)
        {
            try
            {
                //var exist = await _RepoRecepcion.Contar(x => x.RepCodigo == recepcionCompra.RepCodigo && x.RecSecuencia == recepcionCompra.RecSecuencia);


                recepcionCompra.RecSecuencia = await _RepoRecepcion.Contar(x => x.RepCodigo == recepcionCompra.RepCodigo) + 1;
                recepcionCompra.RecReferencia = $"{recepcionCompra.RepCodigo}- {recepcionCompra.RecSecuencia}";
                recepcionCompra.Rowguid = Guid.NewGuid();

                var newRecord = _mapper.Map<RecepcionesProducto>(recepcionCompra);
                var result = await _RepoRecepcion.Agregar(newRecord);

                return true;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<IEnumerable<RecepcionesProductoDTO>> ExecuteGetComprasStoreProcedure(string comReferencia)
        {
            try
            {

                string storeProcedureName = "sp_GetRecepcionesProductos";
                string[] parameters = new string[] { "@ComReferencia" };
                string[] values = new string[] { comReferencia };

                RecepcionesProducto entidad = null;

                var storeResult = await _RepoRecepcion.ExecuteProcedureNonQuery(entidad, storeProcedureName, parameters, values);

                var newRecord = _mapper.Map<IEnumerable<RecepcionesProductoDTO>>(storeResult);

                return newRecord;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<RecepcionesProductoDTO> Cerrar(RecepcionesProductoDTO recepcionCompra)
        {
            try
            {
                //*** Cerrar Compra //
                var _param = new ParametrosDeQuery<ComprasProducto>(1, 100)
                {
                    Where = x => x.RepCodigo == recepcionCompra.RepCodigo && x.ComSecuencia == recepcionCompra.RecSecuencia
                };

                var compra = await _repoCompra.EncontrarPor(_param);

                if (compra == null)
                {
                    throw new ApplicationException("No existe compra con esta secuencia!");
                }

                var myCompra = compra.First();

                myCompra.ComEstatusRecepcion = ((int)recepcionCompra.ComEstadoCompra).ToString(); //Cerrada
                myCompra.ComFechaActualizacion = DateTime.Now;

                await _repoCompra.Actualizar(myCompra);

                /// actualizar recepcion
                var _param1 = new ParametrosDeQuery<RecepcionesProducto>(1, 100)
                {
                    Where = x => x.RepCodigo == recepcionCompra.RepCodigo && x.RecEstado == (int)EstatusRecepcionProductos.Abierta
                };

                var recepciones = await _RepoRecepcion.EncontrarPor(_param1);

                if (recepciones == null)
                {
                    throw new ApplicationException("No existe recepcion con esta secuencia!");
                }

                var newRecord = recepciones.First();
                newRecord = _mapper.Map<RecepcionesProducto>(recepcionCompra);

                newRecord.RecFechaActualizacion = DateTime.Now;
                newRecord.RecEstado = (int)EstatusRecepcionProductos.Cerrada; // Cerrada

                var result = await _RepoRecepcion.Actualizar(newRecord);

                return recepcionCompra;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<RecepcionesProductoDTO> Actualizar(RecepcionesProductoDTO recepcionCompra)
        {
            try
            {
                var newRecord = _mapper.Map<RecepcionesProducto>(recepcionCompra);

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
        public async Task<RecepcionesProductosDetalleDTO> GuradarDetalle(RecepcionesProductosDetalleDTO recepcionDetalle)
        {
            try
            {
                // Actualizar Lote 
                // TODO: validar campo
                if (recepcionDetalle.RecDestino == ((int)LoteDestinoEnum.LoteFermentacion).ToString()) // Fermentacion
                {
                    using (var _lotFermentacion = new LoteFermentacionServices(_mapper))
                    {
                        var lote = new LotesFermentacionDetalleDTO();

                        // TODO: validar campo
                        lote.LotFermentacion = recepcionDetalle.RecReferencia;
                        lote.ComReferencia = recepcionDetalle.ComReferencia;

                        await _lotFermentacion.GuardarDetalle(lote);
                    }
                }
                else
                {
                    using (var _lotSecadoNatural = new LoteSecadoNaturalServices(_mapper))
                    {
                        var lote = new LotesSecadoNaturalDetalleDTO()
                        {
                            // TODO: validar campo
                            LotSecadoManual = recepcionDetalle.RecReferencia,
                            ComReferencia = recepcionDetalle.ComReferencia,
                        };

                        await _lotSecadoNatural.GuardarDetalle(lote);
                    }
                };

                // Insertar detalle recepcion 
                //// verificar si existe cabecera
                RecepcionesProductoDTO RecepcionesProductosDTO;
                var _parampro = new ParametrosDeQuery<RecepcionesProducto>(1, 100)
                {
                    Where = x => x.RepCodigo == recepcionDetalle.RepCodigo && x.RecEstado == (int)EstatusRecepcionProductos.Abierta // Abierta
                };

                var recepcionAbierta = await _RepoRecepcion.EncontrarPor(_parampro);

                if (recepcionAbierta.Count() == 0)
                {
                    RecepcionesProductosDTO = new RecepcionesProductoDTO()
                    {
                        RepCodigo = recepcionDetalle.RepCodigo,
                        Comreferencia = recepcionDetalle.RecReferencia,
                        RecFecha = recepcionDetalle.RecFecha,
                        RecEstado = (int)EstatusRecepcionProductos.Abierta,
                        UsuiniciosesionCreacion = recepcionDetalle.UsuInicioSesion,
                    };

                    var Recepcionresult = await Guardar(RecepcionesProductosDTO);
                }
                else
                {
                    RecepcionesProductosDTO = _mapper.Map<RecepcionesProductoDTO>(recepcionAbierta.First());
                }

                // Insertar Detalle
                recepcionDetalle.RecSecuencia = RecepcionesProductosDTO.RecSecuencia;
                recepcionDetalle.RecPosicion = await _RepoRecepcionDetalle.Contar(x =>
                                                x.RepCodigo == recepcionDetalle.RepCodigo
                                                && x.RecSecuencia == recepcionDetalle.RecSecuencia) + 1;

                recepcionDetalle.Rowguid = Guid.NewGuid();

                var newRecord = _mapper.Map<RecepcionesProductosDetalle>(recepcionDetalle);

                var result = await _RepoRecepcionDetalle.Agregar(newRecord);

                // Actualizar el producto de la compra recibido
                var _param = new ParametrosDeQuery<ComprasProductosDetalle>(1, 100)
                {
                    Where = x => x.ComReferencia == recepcionDetalle.ComReferencia && x.RepCodigo == recepcionDetalle.RepCodigo
                };

                var compradet = _repoCompraDet.EncontrarPor(_param).Result.First();

                if (compradet == null)
                {
                    throw new ApplicationException("No existe este producto en la compra!");
                }

                compradet.ComEstatusDetalle = (int)EstatusComprasProductosDetalle.Recibido; // Recibido;

                compradet.ComFechaActualizacion = DateTime.Now;
                await _repoCompraDet.Actualizar(compradet);

                return recepcionDetalle;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
