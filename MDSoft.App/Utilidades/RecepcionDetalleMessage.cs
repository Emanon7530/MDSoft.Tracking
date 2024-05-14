using Tracking.DTOs;
using Tracking.Modelos;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDSoft.Tracking.Services.DTO;

namespace Tracking.Utilidades
{
    class RecepcionDetalleMessage : ValueChangedMessage<RecepcionesComprasDetalleDTO>
    {
        public RecepcionDetalleMessage(RecepcionesComprasDetalleDTO value) : base(value)
        {
        }
    }
}
