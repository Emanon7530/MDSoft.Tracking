using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services.Dto;
using MDSoft.Tracking.Services.DTO;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Tracking.DataAccess;

namespace Tracking.Services
{
    public class APIManager : IAPIManager
    {
        private HttpClientHandler handler;
        private HttpClient _client;
        private string url = null;
        //public HttpClient Client { get { return _client; } }

        private readonly string comprasProductosController = "ComprasProductos";
        private readonly string lotesController = "Lotes";
        private readonly string recepcionComprasController = "RecepcionesProductos";

        private readonly TrackingDbContext _context;

        public APIManager(TrackingDbContext context)
        {
            //var dbContext = new TrackingDbContext();

            _context = context;

            if (_context.SettingData.Count() > 0)
            {
                InitializeAPI();
            };
        }

        private void InitializeAPI()
        {
            url = _context.SettingData.FirstOrDefault().urlApi;

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
            => true;

        public async Task<List<ComprasProductoDTO>> GetComprasPendientes(DateTime fromDate, DateTime toDate)
        {
            string endPoint = $"{comprasProductosController}/GetPendingByDate?FromDate={fromDate.ToString("yyyy/MM/dd")}&ToDate={toDate.ToString("yyyy/MM/dd")}";

            if (string.IsNullOrEmpty(url))
            {
                InitializeAPI();
            }

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
                if (string.IsNullOrEmpty(url))
                {
                    InitializeAPI();
                }

                string endPoint = $"{comprasProductosController}/sp_GetComprasPendientes";

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

        public async Task<IEnumerable<ComprasProductoDTO>> sp_GetComprasHistoricoPendientes()
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    InitializeAPI();
                }

                string endPoint = $"{comprasProductosController}/sp_GetComprasHistoricoPendientes";

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
            if (string.IsNullOrEmpty(url))
            {
                InitializeAPI();
            }

            string endPoint = $"{comprasProductosController}/GetCompraByTicket?repCodigo={repCodigo}&comSecuencia={comSecuencia}";

            var response = await _client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responsedetail = await response.Content.ReadAsStringAsync();
            var compras = JsonConvert.DeserializeObject<ComprasProductoDTO>(responsedetail);

            return compras;
        }

        public async Task<IEnumerable<ComprasProductosDetalleDTO>> GetProductInCompraByCode(string repCodigo, int comSecuencia, int proID)
        {

            if (string.IsNullOrEmpty(url))
            {
                InitializeAPI();
            }

            string endPoint = $"{comprasProductosController}/GetProductInCompraByCode?repCodigo={repCodigo}&comSecuencia={comSecuencia}&productId={proID}";

            var response = await _client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responsedetail = await response.Content.ReadAsStringAsync();
            var compras = JsonConvert.DeserializeObject<IEnumerable<ComprasProductosDetalleDTO>>(responsedetail);

            return compras;
        }

        public async Task<IEnumerable<TipoProductoDTO>> GetAllTipoProducto()
        {

            if (string.IsNullOrEmpty(url))
            {
                InitializeAPI();
            }

            string endPoint = $"{comprasProductosController}/GetAllTipoProducto";

            var response = await _client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responsedetail = await response.Content.ReadAsStringAsync();
            var compras = JsonConvert.DeserializeObject<IEnumerable<TipoProductoDTO>>(responsedetail);

            return compras;
        }

        public async Task<ComprasProductosDetalleDTO> GetProductInCompraByReference(string repCodigo, int comSecuencia, string comReferencia)
        {
            if (string.IsNullOrEmpty(url))
            {
                InitializeAPI();
            }

            string endPoint = $"{comprasProductosController}/GetProductInCompraByReference?repCodigo={repCodigo}&comSecuencia={comSecuencia}&comReferencia={comReferencia}";

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
            if (string.IsNullOrEmpty(url))
            {
                InitializeAPI();
            }

            string endPoint = $"{lotesController}/GuardarLoteFermentacion";

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

        public async Task<bool> LoteFermentacionExists(string loteFermentacion)
        {
            if (string.IsNullOrEmpty(url))
            {
                InitializeAPI();
            }

            string endPoint = $"{lotesController}/LoteFermentacionExists?lotNumber={loteFermentacion}";

            //HTTP POST
            bool exists = false;

            var response = await _client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var responsedetail = await response.Content.ReadAsStringAsync();

            exists = JsonConvert.DeserializeObject<bool>(responsedetail);

            return exists;
        }

        public async Task<LotesSecadoNaturalDTO> GuardarLoteSecadoNatural(LotesSecadoNaturalDTO loteFermentacion)
        {
            if (string.IsNullOrEmpty(url))
            {
                InitializeAPI();
            }

            string endPoint = $"{lotesController}/GuardarLoteSecadoNatural";

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

        public async Task<bool> LoteSecadoNaturalExists(string loteSecadoNatural)
        {
            if (string.IsNullOrEmpty(url))
            {
                InitializeAPI();
            }

            string endPoint = $"{lotesController}/LoteSecadoNaturalExits?lotNumber={loteSecadoNatural}";

            //HTTP POST
            bool exists = false;

            var response = await _client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var responsedetail = await response.Content.ReadAsStringAsync();

            exists = JsonConvert.DeserializeObject<bool>(responsedetail);

            return exists;

        }

        public async Task<RecepcionesProductoDTO> GuardarRecepcion(RecepcionesProductoDTO recepcionDetalle)
        {
            if (string.IsNullOrEmpty(url))
            {
                InitializeAPI();
            }

            string endPoint = $"{recepcionComprasController}/Guardar";

            //HTTP POST
            RecepcionesProductoDTO compras = new RecepcionesProductoDTO();

            var content = new StringContent(JsonConvert.SerializeObject(recepcionDetalle), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(endPoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responsedetail = await response.Content.ReadAsStringAsync();
                compras = JsonConvert.DeserializeObject<RecepcionesProductoDTO>(responsedetail);

            }
            return compras;
        }

        public async Task<RecepcionesProductosDetalleDTO> GuradarDetalleRecepcion(RecepcionesProductosDetalleDTO recepcionDetalle)
        {
            try
            {

                if (string.IsNullOrEmpty(url))
                {
                    InitializeAPI();
                }

                string endPoint = $"{recepcionComprasController}/GuardarDetalle";

                //HTTP POST
                RecepcionesProductosDetalleDTO compras = new RecepcionesProductosDetalleDTO();

                var content = new StringContent(JsonConvert.SerializeObject(recepcionDetalle), Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(endPoint, content);

                if (response.IsSuccessStatusCode)
                {
                    var responsedetail = await response.Content.ReadAsStringAsync();
                    compras = JsonConvert.DeserializeObject<RecepcionesProductosDetalleDTO>(responsedetail);
                }

                return compras;
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public async Task<RecepcionesProductoDTO> ActualizarRecepcion(RecepcionesProductoDTO recepcionDetalle)
        {
            if (string.IsNullOrEmpty(url))
            {
                InitializeAPI();
            }

            string endPoint = $"{recepcionComprasController}/Cerrar";

            //HTTP POST
            RecepcionesProductoDTO recepcion = new RecepcionesProductoDTO();

            var content = new StringContent(JsonConvert.SerializeObject(recepcionDetalle), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(endPoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responsedetail = await response.Content.ReadAsStringAsync();
                recepcion = JsonConvert.DeserializeObject<RecepcionesProductoDTO>(responsedetail);

            }
            return recepcion;
        }

        public async Task<IEnumerable<RecepcionesProductosDetalleDTO>> GetRecepcionesDetalle(int recSecuencia)
        {
            if (string.IsNullOrEmpty(url))
            {
                InitializeAPI();
            }

            string endPoint = $"{comprasProductosController}/GetRecepcionesDetalle?RecSecuencia={recSecuencia}";

            var response = await _client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responsedetail = await response.Content.ReadAsStringAsync();

            var compras = JsonConvert.DeserializeObject<IEnumerable<RecepcionesProductosDetalleDTO>>(responsedetail);

            return compras;
        }

        public async Task<IEnumerable<RecepcionesProductosDetalleDTO>> GuradarLinear(RecepcionesProductosDetalleDTO recepcionDetalle)
        {
            try
            {

                if (string.IsNullOrEmpty(url))
                {
                    InitializeAPI();
                }

                string endPoint = $"{recepcionComprasController}/GuardarLinear";

                //HTTP POST
                IEnumerable<RecepcionesProductosDetalleDTO> compras = new List<RecepcionesProductosDetalleDTO>();

                var content = new StringContent(JsonConvert.SerializeObject(recepcionDetalle), Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(endPoint, content);

                if (response.IsSuccessStatusCode)
                {
                    var responsedetail = await response.Content.ReadAsStringAsync();
                    compras = JsonConvert.DeserializeObject<IEnumerable<RecepcionesProductosDetalleDTO>>(responsedetail);
                }

                return compras;
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
