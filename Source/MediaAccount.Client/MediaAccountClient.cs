using System;
using System.Net.Http;
using System.Threading.Tasks;
using Krowiorsch.MediaAccount.Model;
using Newtonsoft.Json;

namespace Krowiorsch.MediaAccount
{
    public class MediaAccountClient : IDisposable
    {
        readonly string _userAgent;
        readonly string _apiKey;
        readonly HttpClient _httpClient;

        public MediaAccountClient(string apiKey, Uri baseEndpoint)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient { BaseAddress = baseEndpoint };
            _userAgent = string.Format("MediaAccountClient ({0})", GetType().Assembly.GetName().Version);
        }

        public async Task<Article> GetByIdAsync(long id)
        {
            var message = Create(string.Format("api/v2/Articles/{0}", id));

            var result = await _httpClient.SendAsync(message);

            result.EnsureSuccessStatusCode();

            var json = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Article>(json);
        }

        public async Task<Article[]> GetList()
        {
            // http://test.api.media-account2.de:80/api/v2/Articles?typ=Importdatum&von=1&bis=2
            // ImportDatum
            // Erscheinungsdatum
            // Selektionsdatum
            // Lieferdatum
            // Updatedatum
            // Digitalisierungsdatum

            // von Datetime
            // bis Datetime

            // page: number

            return null;

        }

        HttpRequestMessage Create(string endpoint)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, endpoint);
            message.Headers.Add("api_key", _apiKey);
            message.Headers.Add("User-Agent", _userAgent);
            message.Headers.Add("Accept", "application/json");

            return message;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}