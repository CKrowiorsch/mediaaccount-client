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
        _userAgent = $"MediaAccountClient ({GetType().Assembly.GetName().Version})";
    }

    public record ScrollResponse
    {
        public long AnzahlGesamt { get; set; }
        public long AnzahlVerblieben { get; set; }
        public string? NaechsterCursor { get; set; }
        public string? NaechsterAbrufUrl { get; set; }

        public Article[]? Liste { get; set; }

        // ApiKey ist nicht Teil der API-Antwort, sondern wird hier für die interne Verwendung hinzugefügt, um den Schlüssel für die nächsten Anfragen zu speichern.
        public string ApiKey { get; set; } = null!;
    }

    public async Task<ScrollResponse> SendRequest(string apiKey, DateTime importiertAb, int batchSize, IDictionary<string, string>? parameter = null, CancellationToken cancellation = default)
    {
        var parameters = new Dictionary<string, string>(parameter ?? new Dictionary<string, string>())
        {
            ["anzahl"] = batchSize.ToString(),
            ["importiertAbDatum"] = importiertAb.ToString("s")
        };

        var url = BuildUrl("v2/artikel_stream", parameters);
        var request = CreateRequest(url, apiKey);

        using var response = await _client.SendAsync(request, cancellation);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return DeserializeResponse(json) with { ApiKey = apiKey };
    }

    public async Task<ScrollResponse> SendRequest(string apiKey, string cursor, int batchSize, IDictionary<string, string>? parameter = null, CancellationToken cancellation = default)
    {
        var parameters = new Dictionary<string, string>(parameter ?? new Dictionary<string, string>())
        {
            ["anzahl"] = batchSize.ToString(),
            ["cursor"] = cursor
        };

        var url = BuildUrl("v2/artikel_stream", parameters);
        var request = CreateRequest(url, apiKey);

        using var response = await _client.SendAsync(request, cancellation);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return DeserializeResponse(json) with { ApiKey = apiKey };
    }

    static ScrollResponse DeserializeResponse(string json)
    {
        return JsonConvert.DeserializeObject<ScrollResponse>(json)
               ?? throw new JsonSerializationException("Result kein gültiges Json");
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
        var request = CreateRequest(scroll.NaechsterAbrufUrl, scroll.ApiKey);

        using var response = await _client.SendAsync(request, cancellation);

        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return DeserializeResponse(json) with { ApiKey = scroll.ApiKey };
    }

    static string BuildUrl(string path, Dictionary<string, string>? parameters)
    {
        if (parameters == null || parameters.Count == 0)
            return path;

        var queryString = string.Join("&", parameters.Select(kvp =>
            $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));

        return $"{path}?{queryString}";
    }

    HttpRequestMessage CreateRequest(string url, string apiKey)
    {
        var message = new HttpRequestMessage(HttpMethod.Get, url);
        message.Headers.UserAgent.ParseAdd(_userAgent);
        message.Headers.Add("Accept", "application/json");
        message.Headers.Add("api_key", apiKey);
        return message;
    }
}