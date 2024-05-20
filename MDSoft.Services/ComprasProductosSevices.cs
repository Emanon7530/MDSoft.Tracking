﻿using AutoMapper;
using MDSoft.Data.Common;
using MDSoft.Data.Interface;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Model.Model;
using MDSoft.Tracking.Services.Dto;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
using MDSoft.Tracking.Services.Repository;
using Microsoft.EntityFrameworkCore;
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

        IMapper _mapper;

        public ComprasProductosSevices(IMapper mapper)
        {
            _RepoCompras = new ComprasProductosRepository();
            _RepoDetalle = new ComprasProductosDetalleRepository();
            _RepoProducto = new Repositorio<Producto>();
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

            _param.Where = x => x.ComEstatus == 2;
            _param.OrderBy = x => x.ComFecha.Value;

            IEnumerable<ComprasProducto> compra = await _RepoCompras.EncontrarPor(_param);

            if (compra.Count() > 0)
            {
                result = _mapper.Map<IEnumerable<ComprasProductoDTO>>(compra);
            }

            return result;

        }

        public async Task<IEnumerable<ComprasRepresentante>> sp_GetComprasPendientes()
        {
            IEnumerable<ComprasRepresentante> result = null;

            var compra = await _RepoCompras.sp_GetComprasPendientes();

            return compra;
        }

        public async Task<ComprasProductoDTO> GetCompraByTicket(string comReferencia )
        {
            ComprasProductoDTO result = null;

            var _param = new ParametrosDeQuery<ComprasProducto>(1, 100);

            _param.Where = x => x.ComReferencia.Equals(comReferencia);

            var compra = await _RepoCompras.EncontrarPor(_param);

            if (compra != null)
            {
                result = _mapper.Map<ComprasProductoDTO>(compra.FirstOrDefault());
            }

            return result;

        }

        public async Task<ComprasProductoDTO> CerrarCompra(ComprasProductoDTO compra)
        {
            try
            {
                var editCompra = _mapper.Map<ComprasProducto>(compra);

                editCompra.ComEstatus = 1;

                var result = await _RepoCompras.Actualizar(editCompra);

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

            result = _mapper.Map<ComprasProductosDetalleDTO>(compra.FirstOrDefault());

            var _paramProd = new ParametrosDeQuery<Producto>(1, 100);

            _paramProd.Where = x => x.ProId == result.ProId;

            result.ProDescripcion = _RepoProducto.EncontrarPor(_paramProd).Result.Select(x => x.ProDescripcion).First();
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
