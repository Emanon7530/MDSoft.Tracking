using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Services.Common
{
    public class RecepcionesComprasFilter
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? Id { get; set; }
        public int? IdRepresentante { get; set; }
        public int? IdVendedor { get; set; }
        public string? Usuario { get; set; }
        public string? ComReferencia { get; set; }
    }
}
