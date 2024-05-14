using Tracking.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDSoft.Tracking.Services.DTO;

namespace Tracking.Utilidades
{
    public class ProductoResult
    {
        public bool esCrear { get; set; }
        public ProductoDTO producto { get; set; }
    }
}
