using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Model;

namespace Tracking.Utilidades
{
    public class RecepcionCompraResult 
    {
        public bool esCrear { get; set; }
        public RecepcionesProductoDTO recepcion { get; set; }

    }
}
