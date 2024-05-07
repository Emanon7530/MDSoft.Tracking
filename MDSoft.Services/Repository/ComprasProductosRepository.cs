using MDSoft.Data.Common;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
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
    public class ComprasProductosRepository : Repositorio<ComprasProducto>, IComprasProductosRepository
    {
        public ComprasProductosRepository() : base()
        {

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

    }
}
