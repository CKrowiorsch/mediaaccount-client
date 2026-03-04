using System.Net.Http.Headers;
using Krowiorsch.MediaAccount.Model.V2;
using Newtonsoft.Json;

namespace Krowiorsch.MediaAccount;

public class MediaAccountCursorClient
{
    readonly HttpClient _client;
    readonly string _userAgent;

    public MediaAccountCursorClient(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _client.BaseAddress ??= Globals.EndpointProduction;
        if (!_client.DefaultRequestHeaders.Contains("api_key")) throw new ArgumentException("Api key is missing in the HttpClient headers.", nameof(client));
        _userAgent = $"MediaAccountClient ({GetType().Assembly.GetName().Version})";
    }

    public record ScrollResponse
    {
        public long AnzahlGesamt { get; set; }
        public long AnzahlVerblieben { get; set; }
        public string? NaechsterCursor { get; set; }
        public string? NaechsterAbrufUrl { get; set; }

        public Article[]? Liste { get; set; }
    }

    public async Task<ScrollResponse> SendRequest(DateTime importiertAb, int batchSize, IDictionary<string, string>? parameter = null, CancellationToken cancellation = default)
    {
        var parameters = new Dictionary<string, string>(parameter ?? new Dictionary<string, string>())
        {
            { "anzahl", batchSize.ToString() },
            { "importiertAbDatum", importiertAb.ToString("s") },
        };

        var url = BuildUrl("v2/artikel_stream", parameters);
        var request = CreateRequest(url);

        using var response = await _client.SendAsync(request, cancellation);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return DeserializeResponse(json);
    }

    public async Task<ScrollResponse> SendRequest(string cursor, int batchSize, IDictionary<string, string>? parameter = null, CancellationToken cancellation = default)
    {
        var parameters = new Dictionary<string, string>(parameter ?? new Dictionary<string, string>())
        {
            { "anzahl", batchSize.ToString() },
            { "cursor", cursor },
        };

        var url = BuildUrl("v2/artikel_stream", parameters);
        var request = CreateRequest(url);

        using var response = await _client.SendAsync(request, cancellation);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return DeserializeResponse(json);
    }

    static ScrollResponse DeserializeResponse(string json)
    {
        return JsonConvert.DeserializeObject<ScrollResponse>(json)
               ?? throw new InvalidProgramException("Result kein gültiges Json");
    }

    public async Task<ScrollResponse> SendRequest(ScrollResponse scroll, CancellationToken cancellation)
    {
        if (scroll.NaechsterAbrufUrl is null)
        {
            return new ScrollResponse
            {
                Liste = Array.Empty<Article>(),
                AnzahlGesamt = scroll.AnzahlGesamt,
                NaechsterAbrufUrl = null,
                NaechsterCursor = null
            };
        }
        var request = CreateRequest(scroll.NaechsterAbrufUrl);

        using var response = await _client.SendAsync(request, cancellation);

        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return DeserializeResponse(json);
    }

    static string BuildUrl(string path, Dictionary<string, string>? parameters)
    {
        if (parameters == null || parameters.Count == 0)
            return path;

        var queryString = string.Join("&", parameters.Select(kvp =>
            $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));

        return $"{path}?{queryString}";
    }

    HttpRequestMessage CreateRequest(string url)
    {
        var message = new HttpRequestMessage(HttpMethod.Get, url);
        message.Headers.UserAgent.ParseAdd(_userAgent);
        message.Headers.Add("Accept", "application/json");
        return message;
    }


    public Task<Article> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}