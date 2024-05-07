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
    public class Compra
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdVendedor { get; set; }
        public int IdRepresentante { get; set; }
        public decimal Peso { get; set; }
        public decimal Cantidad { get; set; }
        public string CodigoCompra { get; set; }

        public virtual ICollection<DetalleCompra> detalleCompra { get; set; } = new List<DetalleCompra>();
    }
}
