using Tracking.DTOs;
using Tracking.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Utilidades
{
    public class RecepcionResult
    {
        public bool esCrear { get; set; }
        public RecepcionDTO recepcion { get; set; }
    }
}
