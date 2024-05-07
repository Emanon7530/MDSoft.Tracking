using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNComm.Common.Exceptions
{
    public class TicketCompraRecibidoException : Exception
    {
        public TicketCompraRecibidoException()
        {
        }

        public TicketCompraRecibidoException(string TicketNumber) 
            : base($"El Ticket {TicketNumber} ya fue recibido!")
        {

        }
        public TicketCompraRecibidoException(string TicketNumber, Exception InnerException)
            : base($"El Ticket {TicketNumber} ya fue recibido!", InnerException)
        {

        }

    }

}
