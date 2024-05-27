using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDSoft.Tracking.Services.DTO;

namespace Tracking.Utilidades
{
    public class RecepcionCompraResult 
    {
        public bool esCrear { get; set; }
        public RecepcionesCompraDTO recepcion { get; set; }

    }
}
