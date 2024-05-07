using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Utilidades
{
    class RecepcionMessage : ValueChangedMessage<RecepcionResult>
    {
        public RecepcionMessage(RecepcionResult value) : base(value)
        {

        }
    }
}
