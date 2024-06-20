using MDSoft.Tracking.Services.DTO;
using Tracking.Handlers;
using Newtonsoft.Json;
using MDSoft.Tracking.Services.Repository;
using System.Net.Http.Json;
using Azure;
using System.Net.Http;
using System.Text;
using NuGet.Protocol;

namespace Tracking.Services
{
    public static class APIManager
    {
        private static HttpsClientHandlerService handler = new HttpsClientHandlerService();

        private static HttpClient _client = new HttpClient(handler.GetPlatformMessageHandler())
        {
            BaseAddress = new Uri("https://10.0.2.2:7033/")
        };

        public static HttpClient Client { get { return _client; } }

        public static async Task<List<ComprasProductoDTO>> GetComprasPendientes(DateTime fromDate, DateTime toDate)
        {
            string endPoint = $"ComprasProductos/GetPendingByDate?FromDate={fromDate.ToString("yyyy/MM/dd")}&ToDate={toDate.ToString("yyyy/MM/dd")}";

            var response = await _client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responsedetail = await response.Content.ReadAsStringAsync();
            var compras = JsonConvert.DeserializeObject<List<ComprasProductoDTO>>(responsedetail);

            return compras;
        }

        public static async Task<IEnumerable<ComprasProductoDTO>> sp_GetComprasPendientes()
        {
            string endPoint = $"ComprasProductos/sp_GetComprasPendientes";

            var response = await _client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responsedetail = await response.Content.ReadAsStringAsync();
            var compras = JsonConvert.DeserializeObject<List<ComprasProductoDTO>>(responsedetail);

            return compras;
        }

        public static async Task<ComprasProductoDTO> GetCompraByTicket(string ComReferencia)
        {
            string endPoint = $"ComprasProductos/GetCompraByTicket?ComReferencia={ComReferencia}";

            var response = await _client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responsedetail = await response.Content.ReadAsStringAsync();
            var compras = JsonConvert.DeserializeObject<ComprasProductoDTO>(responsedetail);

            return compras;
        }

        public static async Task<ComprasProductosDetalleDTO> GetProductInCompraByCode(string repCodigo, int comSecuencia, int proID)
        {
            string endPoint = $"ComprasProductos/GetProductInCompraByCode?compra={repCodigo}&comSecuencia={comSecuencia}&productId={proID}";

            var response = await _client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responsedetail = await response.Content.ReadAsStringAsync();
            var compras = JsonConvert.DeserializeObject<ComprasProductosDetalleDTO>(responsedetail);

            return compras;
        }

        public static async Task<LotesFermentacionDTO> GuardarLoteFermentacion(LotesFermentacionDTO loteFermentacion)
        {
            string endPoint = $"Lotes/GuardarLoteFermentacion";

            //HTTP POST
            LotesFermentacionDTO compras = new LotesFermentacionDTO();

            var putTask = _client.PostAsJsonAsync<LotesFermentacionDTO>(endPoint, loteFermentacion);
            putTask.Wait();

            var response = putTask.Result;
            if (response.IsSuccessStatusCode)
            {
                var responsedetail = await response.Content.ReadAsStringAsync();
                compras = JsonConvert.DeserializeObject<LotesFermentacionDTO>(responsedetail);

            }
            return compras;

        }

        public static async Task<LotesSecadoNaturalDTO> GuardarLoteSecadoNatural(LotesSecadoNaturalDTO loteFermentacion)
        {
            string endPoint = $"Lotes/GuardarLoteSecadoNatural";

            //HTTP POST
            LotesSecadoNaturalDTO compras = new LotesSecadoNaturalDTO();

            var content = new StringContent(JsonConvert.SerializeObject(loteFermentacion), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(endPoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responsedetail = await response.Content.ReadAsStringAsync();
                compras = JsonConvert.DeserializeObject<LotesSecadoNaturalDTO>(responsedetail);

            }
            return compras;

        }

        public static async Task<RecepcionesCompraDTO> GuardarRecepcion(RecepcionesCompraDTO recepcionDetalle)
        {
            string endPoint = $"Recepcion/Guardar";

            //HTTP POST
            RecepcionesCompraDTO compras = new RecepcionesCompraDTO();

            var content = new StringContent(JsonConvert.SerializeObject(recepcionDetalle), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(endPoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responsedetail = await response.Content.ReadAsStringAsync();
                compras = JsonConvert.DeserializeObject<RecepcionesCompraDTO>(responsedetail);

            }
            return compras;
        }

        public static async Task<RecepcionesComprasDetalleDTO> GuradarDetalleRecepcion(RecepcionesComprasDetalleDTO recepcionDetalle)
        {
            string endPoint = $"Recepcion/GuardarDetalle";

            //HTTP POST
            RecepcionesComprasDetalleDTO compras = new RecepcionesComprasDetalleDTO();

            var content = new StringContent(JsonConvert.SerializeObject(recepcionDetalle), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(endPoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responsedetail = await response.Content.ReadAsStringAsync();
                compras = JsonConvert.DeserializeObject<RecepcionesComprasDetalleDTO>(responsedetail);

            }
            return compras;
        }

        public static async Task<RecepcionesCompraDTO> ActualizarRecepcion(RecepcionesCompraDTO recepcionDetalle)
        {
            string endPoint = $"Recepcion/Cerrar";

            //HTTP POST
            RecepcionesCompraDTO recepcion = new RecepcionesCompraDTO();

            var content = new StringContent(JsonConvert.SerializeObject(recepcionDetalle), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(endPoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responsedetail = await response.Content.ReadAsStringAsync();
                recepcion = JsonConvert.DeserializeObject<RecepcionesCompraDTO>(responsedetail);

            }
            return recepcion;
        }

        public static async Task<IEnumerable<RecepcionesComprasDetalleDTO>> GetRecepcionesDetalle(int recSecuencia)
        {
            string endPoint = $"ComprasProductos/GetRecepcionesDetalle?RecSecuencia={recSecuencia}";

            var response = await _client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responsedetail = await response.Content.ReadAsStringAsync();

            var compras = JsonConvert.DeserializeObject< IEnumerable<RecepcionesComprasDetalleDTO>>(responsedetail);

            return compras;
        }
    }
}
