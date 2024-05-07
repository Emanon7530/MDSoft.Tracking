using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.DTOs
{
    public partial class CategoriaDTO: ObservableObject
    {
        [ObservableProperty]
        public int idCategoria;
        [ObservableProperty]
        public string numeroLote;
        [ObservableProperty]
        public DateTime fecha;
        [ObservableProperty]
        public string nombre;
    }
}
