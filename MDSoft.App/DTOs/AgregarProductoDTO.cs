using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.DTOs
{
    public partial class AgregarProductoDTO : ObservableObject
    {
        // datos recepcion 
        [ObservableProperty]
        public int id ;
        [ObservableProperty]
        public string codigoCompra ;
        [ObservableProperty]
        public CompraDTO compra ;
        [ObservableProperty]
        public int prodId ;
        [ObservableProperty]
        public ProductoDTO producto ;
        [ObservableProperty]
        public decimal pesoCompra ;
        [ObservableProperty]
        public decimal pesoRecibido ;
        [ObservableProperty]
        public int tipoLote ;
        [ObservableProperty]
        public string comReflotFermentacion ;
        [ObservableProperty]
        public string comReflotSecadoMaquina ;

    }
}
