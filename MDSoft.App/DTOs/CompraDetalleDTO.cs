using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.DTOs
{
    public partial class CompraDetalleDTO : ObservableObject
    {
        [ObservableProperty]
        public int idCompra;
        [ObservableProperty]
        public string comReferencia;
        [ObservableProperty]
        public ProductoDTO producto;
        [ObservableProperty]
        public int estatus;
        [ObservableProperty]
        public decimal peso;
    }
}
