using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDSoft.Tracking.Services.Dto;
using MDSoft.Tracking.Services.DTO;

namespace Tracking.Utilidades
{
    public class CompraDetalleResult
    {
        public bool esCrear { get; set; }
        public ComprasProductoDTO CompraDetalle { get; set; }
    }
}
