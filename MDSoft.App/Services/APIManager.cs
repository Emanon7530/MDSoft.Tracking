using MDSoft.Tracking.Services.DTO;
using Tracking.Handlers;
using Newtonsoft.Json;
using MDSoft.Tracking.Services.Repository;
using System.Net.Http.Json;
using Azure;
using System.Net.Http;
using System.Text;
using NuGet.Protocol;
using Tracking.DataAccess;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Tracking.Services
{
    public class APIManager : IAPIManager
    {
        private HttpClientHandler handler;
        private HttpClient _client;
        public HttpClient Client { get { return _client; } }

        public APIManager()
        {
            var dbContext = new TrackingDbContext();

            string url = dbContext.SettingData.FirstOrDefault().urlApi;

            if (string.IsNullOrEmpty(url))
            {
                throw new ApplicationException("Debe configurar el URL de accesoa al API!");
            }

            handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = CertificateCustomValidation,
                CheckCertificateRevocationList = true
            };

            _client = new HttpClient(handler)
            {
                BaseAddress = new Uri(url)
            };
        }

        private bool CertificateCustomValidation(HttpRequestMessage message, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public async Task<List<ComprasProductoDTO>> GetComprasPendientes(DateTime fromDate, DateTime toDate)
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

        public async Task<IEnumerable<ComprasProductoDTO>> sp_GetComprasPendientes()
        {
            try
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
            catch (Exception e)
            {
                throw;
            }

        }

        public async Task<ComprasProductoDTO> GetCompraByTicket(string repCodigo, int comSecuencia)
        {
            string endPoint = $"ComprasProductos/GetCompraByTicket?repCodigo={repCodigo}&comSecuencia={comSecuencia}";

            var response = await _client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responsedetail = await response.Content.ReadAsStringAsync();
            var compras = JsonConvert.DeserializeObject<ComprasProductoDTO>(responsedetail);

            return compras;
        }

        public async Task<ComprasProductosDetalleDTO> GetProductInCompraByCode(string repCodigo, int comSecuencia, int proID)
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

        public async Task<ComprasProductosDetalleDTO> GetProductInCompraByReference(string repCodigo, int comSecuencia, string comReferencia)
        {
            string endPoint = $"ComprasProductos/GetProductInCompraByReference?repCodigo={repCodigo}&comSecuencia={comSecuencia}&comReferencia={comReferencia}";

            var response = await _client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responsedetail = await response.Content.ReadAsStringAsync();
            var compras = JsonConvert.DeserializeObject<ComprasProductosDetalleDTO>(responsedetail);

            return compras;
        }

        public async Task<LotesFermentacionDTO> GuardarLoteFermentacion(LotesFermentacionDTO loteFermentacion)
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

        public async Task<LotesSecadoNaturalDTO> GuardarLoteSecadoNatural(LotesSecadoNaturalDTO loteFermentacion)
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

        public async Task<RecepcionesCompraDTO> GuardarRecepcion(RecepcionesCompraDTO recepcionDetalle)
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

        public async Task<RecepcionesComprasDetalleDTO> GuradarDetalleRecepcion(RecepcionesComprasDetalleDTO recepcionDetalle)
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

        public async Task<RecepcionesCompraDTO> ActualizarRecepcion(RecepcionesCompraDTO recepcionDetalle)
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

        public async Task<IEnumerable<RecepcionesComprasDetalleDTO>> GetRecepcionesDetalle(int recSecuencia)
        {
            string endPoint = $"ComprasProductos/GetRecepcionesDetalle?RecSecuencia={recSecuencia}";

            var response = await _client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responsedetail = await response.Content.ReadAsStringAsync();

            var compras = JsonConvert.DeserializeObject<IEnumerable<RecepcionesComprasDetalleDTO>>(responsedetail);

            return compras;
        }
    }
}
