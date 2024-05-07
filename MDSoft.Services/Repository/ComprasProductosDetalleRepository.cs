using MDSoft.Data.Common;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Model.Model;
using MDSoft.Tracking.Services.Interface;
using Microsoft.EntityFrameworkCore;
using PNComm.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Services.Repository
{
    public class ComprasProductosDetalleRepository : Repositorio<ComprasProductosDetalle>, IComprasProductosDetalleRepository
    {
        public ComprasProductosDetalleRepository() : base()
        {

        }
        public async Task<bool> Cerrar(ComprasProductosDetalle comprasProducto)
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

        public async Task<ComprasProductosDetalle> GetByCodeBar(string comReferencia)
        {
            var _param = new ParametrosDeQuery<ComprasProductosDetalle>(1, 100);

            _param.Where = x => x.ComReferencia == comReferencia;

            var result = await EncontrarPor(_param);

            return result.FirstOrDefault();
        }


        public async Task<ComprasProductosDetalle> GetByTicketNumber(string ticketNumber)
        {
            var _param = new ParametrosDeQuery<ComprasProductosDetalle>(1, 100);

            _param.Where = x => x.ComReferencia == ticketNumber;

            var result = await EncontrarPor(_param);

            return result.FirstOrDefault();
        }
    }
}
