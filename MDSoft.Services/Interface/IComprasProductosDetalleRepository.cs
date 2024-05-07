using MDSoft.Data.Interface;
using MDSoft.Tracking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Services.Interface
{
    public interface IComprasProductosDetalleRepository : IRepositorio<ComprasProductosDetalle>
    {
		Task<ComprasProductosDetalle> GetByTicketNumber(string ticketNumber);
		Task<ComprasProductosDetalle> GetByCodeBar(string comReferencia);
		Task<bool> Cerrar(ComprasProductosDetalle comprasProducto);

    }
}
