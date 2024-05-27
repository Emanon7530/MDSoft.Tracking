using Tracking.Modelos;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Utilidades
{
    class RecepcionCompraMessage : ValueChangedMessage<RecepcionCompraResult>
    {
        public RecepcionCompraMessage(RecepcionCompraResult value) : base(value)
        {
        }
    }
}
