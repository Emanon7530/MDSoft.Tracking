using MDSoft.Data.Interface;
using MDSoft.Tracking.Model;
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
		Task<ComprasProducto> GetByCodeBar(string comReferencia);
		Task<ComprasProducto> GetByRepresentante(string Representante, int Secuencia);
		Task<bool> Cerrar(ComprasProducto comprasProducto);

    }
}
