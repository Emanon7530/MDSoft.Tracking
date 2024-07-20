using Tracking.Modelos;
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
    class RecepcionDetalleLinearMessage : ValueChangedMessage<IEnumerable<RecepcionesProductosDetalleDTO>>
    {
        public RecepcionDetalleLinearMessage(IEnumerable<RecepcionesProductosDetalleDTO> value) : base(value)
        {
        }
    }
}
