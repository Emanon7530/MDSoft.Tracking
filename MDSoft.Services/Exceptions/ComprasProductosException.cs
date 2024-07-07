using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNComm.Common.Exceptions
{
    public class ComprasProductosException : Exception
    {
        public ComprasProductosException()
        {
        }

        public ComprasProductosException(string comReferencia)
            : base($"La Compra {comReferencia} ya fue recibida!")
        {

        }
        public ComprasProductosException(string comReferencia, Exception InnerException)
            : base($"El Compra {comReferencia} ya fue recibida!", InnerException)
        {

        }

    }

    public class ComprasProductosNoExisteException : Exception
    {
        public ComprasProductosNoExisteException()
        {
        }

        public ComprasProductosNoExisteException(string comReferencia)
            : base($"La Compra {comReferencia} no existe!")
        {

        }
        public ComprasProductosNoExisteException(string comReferencia, Exception InnerException)
            : base($"La Compra {comReferencia} not existe!", InnerException)
        {

        }

    }

}
