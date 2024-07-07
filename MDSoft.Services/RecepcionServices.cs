using AutoMapper;
using MDSoft.Data.Common;
using MDSoft.Data.Interface;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services.Common;
using MDSoft.Tracking.Services.DTO;


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
                    _param.Where = x => x.Comreferencia.Contains(Filter.ComReferencia);
                }

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
                //var exist = await _RepoRecepcion.Contar(x => x.RepCodigo == recepcionCompra.RepCodigo && x.RecSecuencia == recepcionCompra.RecSecuencia);

                var newRecord = _mapper.Map<RecepcionesCompra>(recepcionCompra);

                //if (exist <= 0)
                //{
                newRecord.RecSecuencia = await _RepoRecepcion.Contar(x => x.RepCodigo == recepcionCompra.RepCodigo) + 1;

                //TODO recReferencia de done se llena
                newRecord.RecEstado = 2;  // Abierta
                newRecord.RecReferencia = recepcionCompra.RepCodigo + "-" + newRecord.RecSecuencia.ToString();
                newRecord.RechoraInicial = DateTime.Now.ToLocalTime();

                var result = await _RepoRecepcion.Agregar(newRecord);
                //}
                //else
                //{
                //    var result = await _RepoRecepcion.Actualizar(newRecord);
                //}

                recepcionCompra = _mapper.Map<RecepcionesCompraDTO>(newRecord);

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

        public async Task<RecepcionesCompraDTO> Cerrar(RecepcionesCompraDTO recepcionCompra)
        {
            try
            {
                //*** Cerrar Compra //
                var _param = new ParametrosDeQuery<ComprasProducto>(1, 100)
                {
                    Where = x => x.RepCodigo == recepcionCompra.RepCodigo && x.ComSecuencia == recepcionCompra.ComSecuencia
                };

                var compra = await _repoCompra.EncontrarPor(_param);

                if (compra == null)
                {
                    throw new ApplicationException("No existe compra con esta secuencia!");
                }

                var myCompra = compra.First();

                myCompra.ComEstatus = 1; //Cerrada
                myCompra.ComFechaActualizacion = DateTime.Now;

                await _repoCompra.Actualizar(myCompra);

                /// actualizar recepcion
                var _param1 = new ParametrosDeQuery<RecepcionesCompra>(1, 100)
                {
                    Where = x => x.RepCodigo == recepcionCompra.RepCodigo && x.RecEstado == 2
                };

                var recepciones = await _RepoRecepcion.EncontrarPor(_param1);

                if (recepciones == null)
                {
                    throw new ApplicationException("No existe recepcion con esta secuencia!");
                }

                recepcionCompra.RecFechaActualizacion = DateTime.Now;
                recepcionCompra.RecEstado = 1; // Cerrada

                var newRecord = recepciones.First();
                newRecord = _mapper.Map<RecepcionesCompra>(recepcionCompra);

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
                // Actualizar Lote 
                if (recepcionDetalle.AlmCodigo == "1") // Fermentacion
                {
                    _repoFermentacionDet = new Repositorio<LotesFermentacionDetalle>();

                    var lote = new LotesFermentacionDetalle();
                    lote.LotFermentacion = recepcionDetalle.RecReferencia;
                    lote.ComReferencia = recepcionDetalle.ComReferencia;

                    lote.LotFermentacionSecuencia = await _repoFermentacionDet.Contar(x => x.LotFermentacion == recepcionDetalle.RecReferencia) + 1;

                    await _repoFermentacionDet.Agregar(lote);
                }
                else
                {
                    _repoSecadoNaturalDet = new Repositorio<LotesSecadoNaturalDetalle>();
                    var lote = new LotesSecadoNaturalDetalle()
                    {
                        LotSecadoManual = recepcionDetalle.RecReferencia,
                        ComReferencia = recepcionDetalle.ComReferencia,
                        LotPosicion = await _repoSecadoNaturalDet.Contar(x => x.LotSecadoManual == recepcionDetalle.RecReferencia) + 1
                    };

                    await _repoSecadoNaturalDet.Agregar(lote);
                };

                // Insertar detalle recepcion 
                //// verificar si existe cabecera
                RecepcionesCompraDTO recepcionesComprasDTO;
                var _parampro = new ParametrosDeQuery<RecepcionesCompra>(1, 100)
                {
                    Where = x => x.RepCodigo == recepcionDetalle.RepCodigo && x.RecEstado == 2 // Abierta
                };

                var recepcionAbierta = await _RepoRecepcion.EncontrarPor(_parampro);

                if (recepcionAbierta.Count() == 0)
                {
                    recepcionesComprasDTO = new RecepcionesCompraDTO()
                    {
                        RepCodigo = recepcionDetalle.RepCodigo,
                        RecReferencia = recepcionDetalle.RecReferencia,
                        Comreferencia = recepcionDetalle.ComReferencia,
                        RecSecuencia = await _RepoRecepcion.Contar(x => x.RepCodigo == recepcionDetalle.RepCodigo) + 1,
                        RecFecha = DateTime.Now,
                        AlmCodigo = recepcionDetalle.AlmCodigo,
                        RecImpresa = false,
                        RecEstado = 2,
                        OrdId = 1,
                        ComSecuencia = recepcionDetalle.RefSecuencia,
                        RecFechaCreacion = DateTime.Now,
                    };

                    var Recepcionresult = await Guardar(recepcionesComprasDTO);
                }
                else
                {
                    recepcionesComprasDTO = _mapper.Map<RecepcionesCompraDTO>(recepcionAbierta.First());
                }

                // Insertar Detalle
                recepcionDetalle.recepcionesComprasDTO = recepcionesComprasDTO;
                recepcionDetalle.RecSecuencia = recepcionesComprasDTO.RecSecuencia;
                recepcionDetalle.RecPosicion = await _RepoRecepcionDetalle.Contar(x => x.RepCodigo == recepcionDetalle.RepCodigo
                                                                                  && x.RecSecuencia == recepcionDetalle.RecSecuencia) + 1;

                //TODO De donde lleno el RefSecuencia
                //recepcionDetalle.RefSecuencia = recepcionDetalle.RecPosicion.Value;
                recepcionDetalle.Rowguid = Guid.NewGuid();

                var newRecord = _mapper.Map<RecepcionesComprasDetalle>(recepcionDetalle);

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

                compradet.ComEstatusDetalle = 2; // Recibido;
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
