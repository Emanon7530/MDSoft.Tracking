using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Services.Exceptions
{
    public class InvalidUsernameOrPasswordException : Exception
    {
        public InvalidUsernameOrPasswordException()
        {
        }

        public InvalidUsernameOrPasswordException(string Username) : base($"Invalid Username or Password {Username}")
        {

        }

        public InvalidUsernameOrPasswordException(string Username, Exception InnerException) : base($"Invalid Username or Password ({Username}", InnerException)
        {

        }

    }
    public class UserNotFoundWithEmailAddressExcepcion : Exception
    {
        public UserNotFoundWithEmailAddressExcepcion()
        {
        }

        public UserNotFoundWithEmailAddressExcepcion(string emailAddress) : base($"Username not found with {emailAddress}")
        {

        }

        public UserNotFoundWithEmailAddressExcepcion(string emailAddress, Exception InnerException) : base($"Username not found with {emailAddress}", InnerException)
        {

        }

    }

    public class UserBlockedException : Exception
    {
        public UserBlockedException(string Username) : base($" User {Username} is Block!, Please, contacto you Administrator")
        {

        }

        public UserBlockedException(string Username, Exception InnerException) : base($" User {Username} is Block!, Please, contacto you Administrator", InnerException)
        {

        }

    }

    public class InvalidHistoryPassword : Exception
    {
        public InvalidHistoryPassword(string Username) : base($"The password was be use in the pass, cannot be use again, Plase contacto the Administrator")
        {

        }

        public InvalidHistoryPassword(string Username, Exception InnerException) : base($"The password was be use in the pass, cannot be use again, Plase contacto the Administrator", InnerException)
        {

        }
    }


}
