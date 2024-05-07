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
    public class DetalleCompra
    {
        [Key]
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public string ComReferencia { get; set; }
        public virtual Producto Producto { get; set; }
        public decimal Peso { get; set; }
        public int Estatus { get; set; }
        public virtual Compra Compra { get; set; }

    }
}
