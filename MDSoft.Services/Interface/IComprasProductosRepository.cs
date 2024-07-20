using MDSoft.Data.Interface;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Model.StoreProcedure;
using MDSoft.Tracking.Services.Dto;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Services.Interface
{
    public interface IComprasProductosRepository : IRepositorio<ComprasProducto>
    {
		Task<ComprasProducto> GetByTicketNumber(string ticketNumber);
        Task<IEnumerable<ComprasRepresentante>> sp_GetComprasHistoricoPendientes();
        Task<IEnumerable<ComprasRepresentante>> sp_GetComprasPendientes();
        Task<IQueryable> sp_GetProductoEnCompra(string proCodigo, int comSecuencia, int ProId);
		Task<ComprasProducto> GetByCodeBar(string comReferencia);
		Task<ComprasProducto> GetByRepresentante(string Representante, int Secuencia);
		Task<bool> Cerrar(ComprasProducto comprasProducto);
        Task<IEnumerable<TipoProductoDTO>> GetAllTipoProducto();
    }
}
