using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Data.Common
{
    public static class ToolsHelper
    {

        private static ConfigurationManager _confMang = new ConfigurationManager();

        private static string _connctString = _confMang.GetConnectionString("DefaultConnection");

        public static string ConnectString { get => _connctString ?? ""; }

    }
}
