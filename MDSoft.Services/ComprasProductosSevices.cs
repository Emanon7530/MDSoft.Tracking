using AutoMapper;
using MDSoft.Data.Common;
using MDSoft.Data.Interface;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Model.Model;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
using MDSoft.Tracking.Services.Repository;
using Microsoft.EntityFrameworkCore;
using PNComm.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Services
{
    public class ComprasProductosSevices
    {
        IComprasProductosRepository _RepoCompras;
        IComprasProductosDetalleRepository _RepoDetalle;
        IRepositorio<Producto> _RepoProducto;
        IRepositorio<Representante> _RepoRepresentante;

        IMapper _mapper;

        public ComprasProductosSevices(IMapper mapper)
        {
            _RepoCompras = new ComprasProductosRepository();
            _RepoDetalle = new ComprasProductosDetalleRepository();

            _RepoProducto = new Repositorio<Producto>();
            _RepoRepresentante = new Repositorio<Representante>();

            _mapper = mapper;
        }

        public async Task<IEnumerable<ComprasProductoDTO>> GetAll()
        {
            IEnumerable<ComprasProductoDTO> result = null;

            IEnumerable<ComprasProducto> compra = await _RepoCompras.TraerTodos();

            if (compra.Count() > 0)
            {
                result = _mapper.Map<IEnumerable<ComprasProductoDTO>>(compra);
            }

            return result;

        }

        public async Task<IEnumerable<ComprasProductoDTO>> GetPendingByDate()
        {
            IEnumerable<ComprasProductoDTO> result = null;

            var _param = new ParametrosDeQuery<ComprasProducto>(1, 100);

            _param.Where = x => x.ComEstatus == (int)EstatusCompraProductos.Pendiente;
            _param.OrderBy = x => x.ComFecha.Value;

            IEnumerable<ComprasProducto> compra = await _RepoCompras.EncontrarPor(_param);

            if (compra.Count() > 0)
            {
                result = _mapper.Map<IEnumerable<ComprasProductoDTO>>(compra);
            }

            return result;

        }

        public async Task<IEnumerable<ComprasProductoDTO>> sp_GetComprasHistoricoPendientes()
        {
            IEnumerable<ComprasProductoDTO> result = null;

            var compra = await _RepoCompras.sp_GetComprasHistoricoPendientes();

            if (compra.Count() > 0)
            {
                result = _mapper.Map<IEnumerable<ComprasProductoDTO>>(compra);
            }

            return result;
        }


        public async Task<IEnumerable<ComprasProductoDTO>> sp_GetComprasPendientes()
        {
            IEnumerable<ComprasProductoDTO> result = null;

            var compra = await _RepoCompras.sp_GetComprasPendientes();

            if (compra.Count() > 0)
            {
                result = _mapper.Map<IEnumerable<ComprasProductoDTO>>(compra);
            }

            return result;
        }

        public async Task<ComprasProductoDTO> GetCompraByTicket(string repCodigo, int comSecuencia)
        {
            ComprasProductoDTO result = null;

            var _param = new ParametrosDeQuery<ComprasProducto>(1, 100);

            _param.Where = x => x.RepCodigo.Equals(repCodigo) && x.ComSecuencia.Equals(comSecuencia);

            var compra = await _RepoCompras.EncontrarPor(_param);

            if (compra != null)
            {
                result = _mapper.Map<ComprasProductoDTO>(compra.FirstOrDefault());

                var _paramRep = new ParametrosDeQuery<Representante>(1, 100);
                _paramRep.Where = x => x.RepCodigo == repCodigo;

                result.RepNombre = _RepoRepresentante.EncontrarPor(_paramRep).Result.Select(x => x.RepNombre).First();
            }

            return result;

        }

        public async Task<ComprasProductoDTO> CerrarCompra(ComprasProductoDTO compra)
        {
            try
            {
                var editCompra = _mapper.Map<ComprasProducto>(compra);

                editCompra.ComEstatus = compra.ComEstatus;

                var result = await _RepoCompras.Actualizar(editCompra);

                var compras = _mapper.Map<ComprasProductoDTO>(editCompra);

                return compra;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ComprasProductosDetalleDTO> GetProductInCompraByCode(string repCodigo, int comSecuencia, int prodID)
        {
            ComprasProductosDetalleDTO result = null;

            var _param = new ParametrosDeQuery<ComprasProductosDetalle>(1, 100);

            _param.Where = x => x.RepCodigo == repCodigo && x.ComSecuencia == comSecuencia && x.ProId == prodID;

            var compra = await _RepoDetalle.EncontrarPor(_param);

            if (compra.Count() > 0)
            {
                result = _mapper.Map<ComprasProductosDetalleDTO>(compra.FirstOrDefault());

                var _paramProd = new ParametrosDeQuery<Producto>(1, 100);

                _paramProd.Where = x => x.ProId == result.ProId;

                result.ProDescripcion = _RepoProducto.EncontrarPor(_paramProd).Result.Select(x => x.ProDescripcion).First();
            }
            return result;
        }

        public async Task<ComprasProductosDetalleDTO> GetProductInCompraByReference(string repCodigo, int comSecuencia, string comReference)
        {
            ComprasProductosDetalleDTO result = null;

            var _param = new ParametrosDeQuery<ComprasProductosDetalle>(1, 100);

            _param.Where = x => x.RepCodigo == repCodigo && x.ComSecuencia == comSecuencia && x.ComReferencia == comReference && x.ComEstatusDetalle == (int)EstatusComprasProductosDetalle.Pendiente;

            var compra = await _RepoDetalle.EncontrarPor(_param);

            if (compra.Count() > 0)
            {
                result = _mapper.Map<ComprasProductosDetalleDTO>(compra.FirstOrDefault());

                var _paramProd = new ParametrosDeQuery<Producto>(1, 100);

                _paramProd.Where = x => x.ProId == result.ProId;

                result.ProDescripcion = _RepoProducto.EncontrarPor(_paramProd).Result.Select(x => x.ProDescripcion).First();
            }

            return result;
        }


        public async Task<IEnumerable<ComprasProductosDetalleDTO>> GetProductsInCompra(string repCodigo, int comSecuencia)
        {
            var _param = new ParametrosDeQuery<ComprasProductosDetalle>(1, 100);

            _param.Where = x => x.RepCodigo == repCodigo && x.ComSecuencia == comSecuencia;

            IEnumerable<ComprasProductosDetalle> compra = await _RepoDetalle.EncontrarPor(_param);

            var result = _mapper.Map<IEnumerable<ComprasProductosDetalleDTO>>(compra);

            return result;
        }

    }
}
