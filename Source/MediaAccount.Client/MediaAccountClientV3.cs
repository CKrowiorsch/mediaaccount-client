using System.Net.Http;
using System.Threading.Tasks;
using Krowiorsch.MediaAccount.Model;
using Krowiorsch.MediaAccount.Model.V3;
using Krowiorsch.MediaAccount.RequestBuilder;
using Newtonsoft.Json;

namespace Krowiorsch.MediaAccount;

public class MediaAccountClientV3 : IDisposable, IMediaAccountClient<Meldung>
{
    readonly string _userAgent;
    readonly string _apiKey;
    readonly HttpClient _httpClient;

    readonly ArticleListDeserializer _deserializer = new();

    /// <summary>Erzeugt einen Client für den Gegebenen ApiKey. Wenn kein Endpunkt angegeben wird, wird das Produktivsystem benutzt.</summary>
    /// <param name="apiKey">Api key</param>
    /// <param name="baseEndpoint">alternativer Endpoint</param>
    public MediaAccountClientV3(string apiKey, Uri? baseEndpoint = null)
    {
        baseEndpoint ??= Globals.EndpointProduction;

        _apiKey = apiKey;
        _httpClient = new HttpClient { BaseAddress = baseEndpoint };
        _userAgent = $"MediaAccountClient ({GetType().Assembly.GetName().Version})";
    }

    public async Task<Meldung?> GetByIdAsync(string id)
    {
        var message = Create($"api/v3/meldung/{id}");
        var result = await _httpClient.SendAsync(message);

        result.EnsureSuccessStatusCode();

        var json = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Meldung>(json);
    }

    public ArticleListScroll<Meldung> CreateScroll(RequestDateType dateType, DateTime start, DateTime end, int batchSize = 50, string? additionalParameters = null)
    {
        var request = new V3ArticleRequestBuilder(_httpClient.BaseAddress, _apiKey).CreateInitialUrl(dateType, start, end, batchSize, additionalParameters);
        return new ArticleListScroll<Meldung>(this, MoveScroll) { NextPageLink = request };
    }

    internal async Task<bool> MoveScroll(ArticleListScroll<Meldung> scroll)
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