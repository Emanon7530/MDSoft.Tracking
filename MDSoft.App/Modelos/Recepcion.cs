using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Modelos
{
    public class Recepcion
    {
        [Key]
        // datos recepcion 
        public int Id { get; set; }
        public string CodigoCompra { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public int IdVendedor { get; set; }
        public int IdRepresentante { get; set; }
        public decimal PesoCompra { get; set; }
        public decimal PesoRecibido { get; set; }
        public int TipoLote { get; set; }
        public int? LotFermentacion { get; set; }
        public int? LotSecadoMaquina { get; set; }

        public virtual ICollection<RecepcionDetalle> recepcionDetalle { get; set; } = new List<RecepcionDetalle>();
    }
}
