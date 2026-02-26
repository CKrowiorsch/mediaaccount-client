using Krowiorsch.MediaAccount.Model.V2;
using Newtonsoft.Json;

namespace Krowiorsch.MediaAccount;

public class MediaAccountCursorClient(HttpClient client)
{
    public record ScrollResponse
    {
        public long AnzahlGesamt { get; set; }
        public long AnzahlVerbleibed { get; set; }
        public string? NaechsterCursor { get; set; }
        public string? NaechsterAbrufUrl { get; set; }

        public Article[]? Liste { get; set; }
    }

    public async Task<ScrollResponse> SendRequest(DateTime start, int batchSize, IDictionary<string, string>? parameter = null, CancellationToken cancellation = default)
    {
        var parameters = new Dictionary<string, string>(parameter ?? new Dictionary<string, string>())
        {
            { "anzahl", batchSize.ToString() },
            { "startdatum", start.ToString("s") },
        };

        var url = BuildUrl("v2/artikel_stream", parameters);
        var request = CreateRequest(url);

        using var response = await client.SendAsync(request, cancellation);
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

        using var response = await client.SendAsync(request, cancellation);

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
            return new ScrollResponse()
            {
                Liste = Array.Empty<Article>(),
                AnzahlGesamt = scroll.AnzahlGesamt,
                NaechsterAbrufUrl = null,
                NaechsterCursor = null
            };
        }
        var request = CreateRequest(scroll.NaechsterAbrufUrl);

        using var response = await client.SendAsync(request, cancellation);

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

    static HttpRequestMessage CreateRequest(string url)
        => new(HttpMethod.Get, url);


    public Task<Article> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}