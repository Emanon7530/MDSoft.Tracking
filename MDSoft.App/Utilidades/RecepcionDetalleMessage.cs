using Tracking.DTOs;
using Tracking.Modelos;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Utilidades
{
    class RecepcionDetalleMessage : ValueChangedMessage<RecepcionDetalleDTO>
    {
        public RecepcionDetalleMessage(RecepcionDetalleDTO value) : base(value)
        {
        }
    }
}
