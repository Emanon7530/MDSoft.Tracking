using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Modelos
{
    public class SettingsDataDTO : ObservableObject
    {
        public int Id { get; set; }

        public string mdsoftKey { get; set; }

        public string urlApi { get; set; }

    }
}
