using MDSoft.Data.Common;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Model.StoreProcedure;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
using Microsoft.EntityFrameworkCore;
using PNComm.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MDSoft.Tracking.Services.Repository
{
    public class ComprasProductosRepository : Repositorio<ComprasProducto>, IComprasProductosRepository
    {
        Repositorio<Representante> _repoRepresentante;
        Repositorio<VwComprasProducto> _repovwComprasProd;
        Repositorio<VwComprasProductosDetalle> _repovwComprasProdDetalle;

        public ComprasProductosRepository() : base()
        {
            _repoRepresentante = new Repositorio<Representante>();

            _repovwComprasProd = new Repositorio<VwComprasProducto>();
            _repovwComprasProdDetalle = new Repositorio<VwComprasProductosDetalle>();
        }

        public async Task<bool> Cerrar(ComprasProducto comprasProducto)
        {
            var estatusActualizar = await this.Actualizar(comprasProducto);
            if (estatusActualizar > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ComprasProducto> GetByCodeBar(string comReferencia)
        {
            var _param = new ParametrosDeQuery<ComprasProducto>(1, 100);

            _param.Where = x => x.ComReferencia == comReferencia;

            var result = await EncontrarPor(_param);

            return result.FirstOrDefault();
        }

        public async Task<ComprasProducto> GetByRepresentante(string Representante, int Secuencia)
        {
            var _param = new ParametrosDeQuery<ComprasProducto>(1, 100);

            _param.Where = x => x.RepSupervisor == Representante && x.ComSecuencia == Secuencia;

            var result = await EncontrarPor(_param);

            return result.FirstOrDefault();
        }

        public async Task<ComprasProducto> GetByTicketNumber(string ticketNumber)
        {
            var _param = new ParametrosDeQuery<ComprasProducto>(1, 100);

            _param.Where = x => x.ProCodigo == ticketNumber;

            var result = await EncontrarPor(_param);

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<ComprasRepresentante>> sp_GetComprasHistoricoPendientes()
        {
            using (var dbContext = new MovilBusiness5StdContext())
            {
                var entryPoint = await (from ep in dbContext.VwComprasProductos
                                        join d in dbContext.VwComprasProductosDetalles on new { ep.RepCodigo, ep.ComSecuencia } equals new { d.RepCodigo, d.ComSecuencia }
                                        join e in dbContext.Representantes on ep.RepCodigo equals e.RepCodigo
                                        join pr in dbContext.Proveedores on ep.ProCodigo equals pr.ProCodigo
                                        where ep.ComEstatusRecepcion == ((int)EstatusCompraProductos.Pendiente).ToString()
                                        group d by new { ep.RepCodigo, ep.ComReferencia, ep.ComSecuencia, ep.ComFecha, ep.ComEstatus, e.RepNombre, pr.ProNombre, pr.ProCodigo } into grupo
                                        select new ComprasRepresentante
                                        {
                                            RepCodigo = grupo.Key.RepCodigo,
                                            ProCodigo = grupo.Key.ProCodigo,
                                            ComSecuencia = grupo.Key.ComSecuencia,
                                            ComFecha = grupo.Key.ComFecha,
                                            RepNombre = grupo.Key.RepNombre,
                                            ProNombre = grupo.Key.ProNombre,
                                            ComReferencia = grupo.Key.ComReferencia,
                                            ComCantidadDetalle = grupo.Sum(t1 => t1.ComPesoKg),
                                            ComEstatus = grupo.Key.ComEstatus
                                        }).Take(100).ToListAsync();

                return entryPoint;
            }
        }


        public async Task<IEnumerable<ComprasRepresentante>> sp_GetComprasPendientes()
        {
            using (var dbContext = new MovilBusiness5StdContext())
            {
                var entryPoint = await (from ep in dbContext.ComprasProductos
                                        join d in dbContext.ComprasProductosDetalles on new { ep.RepCodigo, ep.ComSecuencia } equals new { d.RepCodigo, d.ComSecuencia }
                                        join e in dbContext.Representantes on ep.RepCodigo equals e.RepCodigo
                                        join pr in dbContext.Proveedores on ep.ProCodigo equals pr.ProCodigo
                                        where ep.ComEstatusRecepcion == ((int)EstatusCompraProductos.Pendiente).ToString()
                                        || ep.ComEstatusRecepcion == ((int)EstatusCompraProductos.RecibidoParcial).ToString()
                                        group d by new { ep.RepCodigo, ep.ComReferencia, ep.ComSecuencia, ep.ComFecha, ep.ComEstatus, e.RepNombre, pr.ProNombre, pr.ProCodigo } into grupo
                                        select new ComprasRepresentante
                                        {
                                            RepCodigo = grupo.Key.RepCodigo,
                                            ProCodigo = grupo.Key.ProCodigo,
                                            ComSecuencia = grupo.Key.ComSecuencia,
                                            ComFecha = grupo.Key.ComFecha,
                                            RepNombre = grupo.Key.RepNombre,
                                            ProNombre = grupo.Key.ProNombre,
                                            ComReferencia = grupo.Key.ComReferencia,
                                            ComCantidadDetalle = grupo.Sum(t1 => t1.ComPrecioKg),
                                            ComEstatus = grupo.Key.ComEstatus
                                        }).Take(100).ToListAsync();

                return entryPoint;
            }
        }

        public async Task<IQueryable> sp_GetProductoEnCompra(string proCodigo, int comSecuencia, int ProId)
        {
            using (var dbContext = (MovilBusiness5StdContext)this.Context)
            {
                IQueryable entryPoint = (from de in dbContext.ComprasProductosDetalles
                                         join ep in dbContext.ComprasProductos on new { de.RepCodigo, de.ComSecuencia } equals new { ep.RepCodigo, ep.ComSecuencia }
                                         join pr in dbContext.Productos on de.ProId equals pr.ProId
                                         join e in dbContext.Representantes on ep.RepCodigo equals e.RepCodigo
                                         where ep.ProCodigo == proCodigo && ep.ComSecuencia == comSecuencia && de.ProId == ProId
                                         select new
                                         {
                                             RepCodigo = ep.RepCodigo,
                                             ComSecuencia = ep.ComSecuencia,
                                             ComFecha = ep.ComFecha,
                                             RepNombre = e.RepNombre,
                                             ComReferencia = de.ComReferencia,
                                             ComCantidadDetalle = ep.ComCantidadDetalle,
                                             ComEstatus = ep.ComEstatus,
                                             ProId = de.ProId,
                                             ProDescripcion = pr.ProDescripcion,
                                             ComCantidad = de.ComCantidad,
                                             ComEstadoProducto = de.ComEstadoProducto

                                         }).Take(10);


                return entryPoint;
            }
        }
    }
}
