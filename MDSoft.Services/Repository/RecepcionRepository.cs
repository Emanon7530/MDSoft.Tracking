using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Services.Repository
{
    public class RecepcionRepository : Repositorio<RecepcionesCompra>, IRecepcionRepository
    {
        public Task<int> Save(RecepcionesCompra recepcion)
        {
            var Id = this.Agregar(recepcion);

            return Id;
        }
        public RecepcionRepository() : base()
        {

        }
 
    }
}
