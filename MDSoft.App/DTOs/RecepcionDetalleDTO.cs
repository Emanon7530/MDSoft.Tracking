using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.DTOs
{
    public partial class RecepcionDetalleDTO : ObservableObject
    {
        [ObservableProperty]
        public string codigoCompra;
        [ObservableProperty]
        public RecepcionDTO recepcion;
        [ObservableProperty]
        public DateTime fechaRecepcion;
        [ObservableProperty]
        public string destino;
        [ObservableProperty]
        public string numeroLote;
        [ObservableProperty]
        public string comReferencia;
        [ObservableProperty]
        public string nombreProducto;
        [ObservableProperty]
        public decimal pesoCompra;
        [ObservableProperty]
        public decimal pesoRecibido;
    }
}
