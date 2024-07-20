using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MDSoft.Tracking.Services.Dto
{
    public class TipoProductoDTO
    {
        public int ProId { get; set; }
        public string ComTipoProducto { get; set; }
        public string ComTipoCertificacion { get; set; }
        public string TipoProducto => $"{ComTipoProducto}/{ComTipoCertificacion}";
    }
}
