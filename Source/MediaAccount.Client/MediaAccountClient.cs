using System;
using System.Net.Http;
using System.Threading.Tasks;
using Krowiorsch.MediaAccount.Model;
using Krowiorsch.MediaAccount.RequestBuilder;
using Newtonsoft.Json;

namespace Krowiorsch.MediaAccount
{
    public class MediaAccountClient : IDisposable
    {
        readonly string _userAgent;
        readonly string _apiKey;
        readonly HttpClient _httpClient;

        readonly ArticleListDeserializer _deserializer = new ArticleListDeserializer();

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



        public async Task<ArticleListScroll> GetList(RequestDateType dateType, DateTimeOffset start, DateTimeOffset end, int page = 0)
        {
            var request = new ArticleRequestBuilder(_httpClient.BaseAddress, _apiKey).Create(dateType, start, end, page);
            var result = await _httpClient.SendAsync(request);
            result.EnsureSuccessStatusCode();

            var scroll = new ArticleListScroll(this);
            _deserializer.DeserializeInto(await result.Content.ReadAsStringAsync(), scroll);
            return scroll;

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
        }


        public ArticleListScroll CreateScroll(RequestDateType dateType, DateTimeOffset start, DateTimeOffset end)
        {
            var request = new ArticleRequestBuilder(_httpClient.BaseAddress, _apiKey).CreateInitialUrl(dateType, start, end);
            return new ArticleListScroll(this) { NextPageLink = request };
        }


        internal async Task<bool> MoveScroll(ArticleListScroll scroll)
        {
            if (string.IsNullOrEmpty(scroll.NextPageLink))
                return false;

            var request = new ArticleScrollBuilder(_httpClient.BaseAddress, _apiKey).Create(scroll);
            var result = await _httpClient.SendAsync(request);
            result.EnsureSuccessStatusCode();
            var json = await result.Content.ReadAsStringAsync();

            return _deserializer.DeserializeInto(json, scroll);
        }

        public async Task<Article[]> LastArticles()
        {
            return await Task.FromResult<Article[]>(null);
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