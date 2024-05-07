using Tracking.DTOs;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Utilidades
{
    class CompraDetalleMessage : ValueChangedMessage<CompraDetalleResult>
    {
        public CompraDetalleMessage(CompraDetalleResult value) : base(value)
        {
        }
    }
}
