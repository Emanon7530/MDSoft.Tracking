using AutoMapper;
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

        IMapper _mapper;

        public ComprasProductosSevices(IMapper mapper)
        {
            _RepoCompras = new ComprasProductosRepository();
            _RepoDetalle = new ComprasProductosDetalleRepository();
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComprasProductoDTO>> GetAll()
        {
            IEnumerable<ComprasProductoDTO> result = null;

            var _param = new ParametrosDeQuery<ComprasProducto>(1, 100);

            IEnumerable<ComprasProducto> compra = await _RepoCompras.EncontrarPor(_param);

            if (compra.Count() > 0)
            {
                result = _mapper.Map<IEnumerable<ComprasProductoDTO>>(compra);
            }

            return result;

        }

        public async Task<ComprasProductoDTO> GetCompraByTicket(string TicketNumber)
        {
            ComprasProductoDTO result = null;

            var _param = new ParametrosDeQuery<ComprasProducto>(1, 100);

            _param.Where = x => x.ComReferencia.Equals(TicketNumber);

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

        public async Task<ComprasProductosDetalleDTO> GetProductInCompraByCode(ComprasProductoDTO comprasProducto, int ProductCode)
        {
            ComprasProductosDetalleDTO result = null;

            var _param = new ParametrosDeQuery<ComprasProductosDetalle>(1, 100);

            _param.Where = x => x.RepCodigo == comprasProducto.RepCodigo && x.ComSecuencia == comprasProducto.ComSecuencia && x.ProId == ProductCode;

            var compra = await _RepoDetalle.EncontrarPor(_param);

            result = _mapper.Map<ComprasProductosDetalleDTO>(compra);

            return result;
        }

    }
}
