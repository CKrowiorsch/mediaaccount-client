using System.Net.Http.Headers;
using Krowiorsch.MediaAccount.Model;
using Krowiorsch.MediaAccount.Model.V2;
using Krowiorsch.MediaAccount.RequestBuilder;
using Newtonsoft.Json;

namespace Krowiorsch.MediaAccount;

public class MediaAccountClientV2 : IMediaAccountClient<Article>
{
    readonly string _userAgent;
    readonly HttpClient _httpClient;

    readonly ArticleListDeserializer _deserializer = new();

    /// <summary>Erzeugt einen Client für den Gegebenen ApiKey. Wenn kein Endpunkt angegeben wird, wird das Produktivsystem benutzt.</summary>
    /// <param name="apiKey">Api key</param>
    /// <param name="baseEndpoint">alternativer Endpoint</param>
    [Obsolete("Use HttpClient Construtkru")]
    public MediaAccountClientV2(string apiKey, Uri? baseEndpoint = null)
    {
        baseEndpoint ??= Globals.EndpointProduction;

        _httpClient = new HttpClient
        {
            BaseAddress = baseEndpoint,
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue("api_key", apiKey)
            }
        };
        _userAgent = $"MediaAccountClient ({GetType().Assembly.GetName().Version})";
    }

    public MediaAccountClientV2(HttpClient client)
    {
        _httpClient = client ?? throw new ArgumentNullException(nameof(client));
        _httpClient.BaseAddress ??= Globals.EndpointProduction;
        if (!_httpClient.DefaultRequestHeaders.Contains("api_key")) throw new ArgumentException("Api key is missing in the HttpClient headers.");
        _userAgent = $"MediaAccountClient ({GetType().Assembly.GetName().Version})";
    }

    public async Task<Article?> GetByIdAsync(string id)
    {
        var message = Create($"api/v2/Articles/{id}");
        var result = await _httpClient.SendAsync(message);

        result.EnsureSuccessStatusCode();

        var json = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Article>(json);
    }

    public ArticleListScroll<Article> CreateScroll(RequestDateType dateType, DateTime start, DateTime end, int batchSize = 50, string? additionalParameters = null)
    {
        var request = new V2ArticleRequestBuilder(_httpClient.BaseAddress).CreateInitialUrl(dateType, start, end, batchSize, additionalParameters);
        return new ArticleListScroll<Article>(this, MoveScroll) { NextPageLink = request };
    }


    internal async Task<bool> MoveScroll(ArticleListScroll<Article> scroll)
    {
        if (string.IsNullOrEmpty(scroll.NextPageLink))
            return false;

        var request = new ArticleScrollBuilder(_httpClient.BaseAddress).Create(scroll);
        var result = await _httpClient.SendAsync(request).ConfigureAwait(false);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();

        return _deserializer.DeserializeInto(json, scroll);
    }

    HttpRequestMessage Create(string endpoint)
    {
        var message = new HttpRequestMessage(HttpMethod.Get, endpoint);
        message.Headers.UserAgent.ParseAdd(_userAgent);
        message.Headers.Add("Accept", "application/json");

        return message;
    }
}