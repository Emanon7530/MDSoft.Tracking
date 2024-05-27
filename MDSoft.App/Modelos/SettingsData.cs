using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Modelos
{
    public class SettingsData
    {
        [Key]
        public int Id { get; set; }
        public string mdsoftKey { get; set; }
        public string urlApi { get; set; }

    }
}
