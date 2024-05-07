using MDSoft.Data.Interface;
using MDSoft.Tracking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Services.Interface
{
    public interface IRecepcionRepository : IRepositorio<RecepcionesCompra>
    {
		Task<int> Save(RecepcionesCompra recepcion);

    }
}
