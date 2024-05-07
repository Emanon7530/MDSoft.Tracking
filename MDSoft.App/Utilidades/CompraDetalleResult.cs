using Tracking.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Utilidades
{
    public class CompraDetalleResult
    {
        public bool esCrear { get; set; }
        public CompraDetalleDTO CompraDetalle { get; set; }
    }
}
