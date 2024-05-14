using MDSoft.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.DataAccess
{
    public static class DataAccessServices
    {
       private static HttpClient _client = new();

        public static HttpClient Client { get { return _client;} }

    }
}
