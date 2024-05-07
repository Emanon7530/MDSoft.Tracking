using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace Tracking.Modelos
{
    public class RecepcionDetalle
    {
        [Key]
        public int RecepcionDetalleId { get; set; }
        public int RecepcionId { get; set; }
        public virtual Recepcion Recepcion { get; set; }
        public string ComReferencia { get; set; }
        public decimal PesoCompra { get; set; }
        public decimal PesoRecibido { get; set; }
    }
}
