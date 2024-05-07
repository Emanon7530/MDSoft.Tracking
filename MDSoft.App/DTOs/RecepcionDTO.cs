using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.DTOs
{
    public partial class RecepcionDTO : ObservableObject
    {
        // datos recepcion 
        [ObservableProperty]
        public int id ;
        [ObservableProperty]
        public string codigoCompra ;
        [ObservableProperty]
        public DateTime fechaRecepcion ;
        [ObservableProperty]
        public int idVendedor ;
        [ObservableProperty]
        public string nombreVendedor ;
        [ObservableProperty]
        public int secuencia ;
        [ObservableProperty]
        public int idRepresentante ;
        [ObservableProperty]
        public string nombreRepresentante ;
        [ObservableProperty]
        public decimal pesoCompra ;
        [ObservableProperty]
        public decimal pesoRecibido ;
        [ObservableProperty]
        public int tipoLote ;
        [ObservableProperty]
        public int? lotFermentacion ;
        [ObservableProperty]
        public int? lotSecadoMaquina ;

        [ObservableProperty]
        public ICollection<RecepcionDetalleDTO> recepcionDetalleDTO  = new List<RecepcionDetalleDTO>();
    }
}
