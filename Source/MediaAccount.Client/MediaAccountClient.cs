using System;
using System.Net.Http;
using System.Threading.Tasks;
using Krowiorsch.MediaAccount.Model;
using Krowiorsch.MediaAccount.RequestBuilder;
using Newtonsoft.Json;

namespace Krowiorsch.MediaAccount
{
    public class MediaAccountClient : IDisposable, IMediaAccountClient
    {
        readonly string _userAgent;
        readonly string _apiKey;
        readonly HttpClient _httpClient;
        readonly ApiVersions _apiVersion;

        readonly ArticleListDeserializer _deserializer = new ArticleListDeserializer();

        /// <summary>Erzeugt einen Client für den Gegebenen ApiKey. Wenn kein Endpunkt angegeben wird, wird das Produktivsystem benutzt.</summary>
        /// <param name="apiKey">Api key</param>
        /// <param name="baseEndpoint">alternativer Endpoint</param>
        public MediaAccountClient(string apiKey, Uri baseEndpoint = null, ApiVersions apiVersion = ApiVersions.Version2)
        {
            baseEndpoint = baseEndpoint ?? Globals.EndpointProduction;

            _apiKey = apiKey;
            _apiVersion = apiVersion;
            _httpClient = new HttpClient { BaseAddress = baseEndpoint };
            _userAgent = $"MediaAccountClient ({GetType().Assembly.GetName().Version})";
        }

        public async Task<Article> GetByIdAsync(string id)
        {
            var message = Create($"api/v2/Articles/{id}");
            var result = await _httpClient.SendAsync(message);

            result.EnsureSuccessStatusCode();

            var json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Article>(json);
        }

        public ArticleListScroll CreateScroll(RequestDateType dateType, DateTimeOffset start, DateTimeOffset end, int batchSize = 50, string additionalParameters = null)
        {
            string request;

            switch (_apiVersion)
            {
                case ApiVersions.Version3:
                    request = new V3ArticleRequestBuilder(_httpClient.BaseAddress, _apiKey).CreateInitialUrl(dateType, start, end, batchSize, additionalParameters);
                    break;
                
                case ApiVersions.Version2:
                default:
                    request = new V2ArticleRequestBuilder(_httpClient.BaseAddress, _apiKey).CreateInitialUrl(dateType, start, end, batchSize, additionalParameters);
                    break;

            }

            return new ArticleListScroll(this) { NextPageLink = request };
        }


        internal async Task<bool> MoveScroll(ArticleListScroll scroll)
        {
            if (string.IsNullOrEmpty(scroll.NextPageLink))
                return false;

            var request = new ArticleScrollBuilder(_httpClient.BaseAddress, _apiKey).Create(scroll);
            var result = await _httpClient.SendAsync(request).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();
            var json = await result.Content.ReadAsStringAsync();

            return _deserializer.DeserializeInto(json, scroll);
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposable)
        {
            _httpClient.Dispose();
        }
    }

    public enum ApiVersions
    {
        Version2,
            
        Version3
    }
}