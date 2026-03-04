using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Krowiorsch.MediaAccount.RequestBuilder;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Krowiorsch.MediaAccount;

public static class Program
{
    static Uri BaseEndpoint = new Uri("http://api.media-account.de");
    static readonly int MaxCountPerScroll = 150;

    public static async Task Main()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}]  {Message} {NewLine}{Exception}", theme: AnsiConsoleTheme.Code)
            .CreateLogger();

        // override BaseEndpoint for testing
        BaseEndpoint = new Uri("http://maprodapi-staging.azurewebsites.net");

        var keyProvider = new ManualKeyProvider();

        for (var i = 0; i < 5; i++)
        {
            Log.Information("Start Iteration-{Iteration} - {Key}", i, keyProvider.Provide());
            await MediaAccountCursorClientAsync(keyProvider.Provide());
            await MediaAccountV2Async(keyProvider.Provide());
        }

        Console.Read();
        Console.WriteLine();
    }

    static async Task MediaAccountCursorClientAsync(string key)
    {
        var duration = Stopwatch.StartNew();
        using var httpClient = new HttpClient
        {
            BaseAddress = BaseEndpoint,
            DefaultRequestHeaders =
            {
                {"api_key", key}
            }
        };

        var client = new IntializeClient().GetCursorClient(httpClient);

        var count = 0;
        var batchCount = 0;
        var startDate = DateTime.Now.Date.AddDays(-14);

        var parameters = new Dictionary<string, string>
        {
            { "geloeschteEinbeziehen", "true" },
            { "stornierteEinbeziehen", "true" },
            { "lm_internerZugriff", "true" }
        };

        Log.Information("Testing CursorClient with IAsyncEnumerable (Batches)");

        await foreach (var batch in client.GetArticleBatchesAsync(startDate, 50, parameters))
        {
            batchCount++;
            Log.Debug("Batch {BatchNumber}: {Count} Articles, Gesamt: {Total}, Verbleibend: {Remaining}",
                batchCount, batch.Articles.Length, batch.AnzahlGesamt, batch.AnzahlVerblieben);

            foreach (var article in batch.Articles)
            {
                Log.Debug("  Article:{Id} - Date:{Date} - Headline:{Headline}",
                    article.Id, article.Importdatum, article.Inhalt.Headline);
            }

            count += batch.Articles.Length;

            if (count >= MaxCountPerScroll)
            {
                break;
            }
        }

        Log.Information("[CursorClient-Batches] Found {Count} Articles in {BatchCount} Batches (Dauer: {Duration} ms)",
            count, batchCount, duration.ElapsedMilliseconds);

        duration.Restart();
        count = 0;

        Log.Information("Testing CursorClient with IAsyncEnumerable (Single Articles)");

        await foreach (var article in client.GetArticlesAsync(startDate, 50, parameters))
        {
            count++;

            Serilog.Log.Debug("Artikel: {Id} - Date:{Date} - Headline:{Headline}", article.Id, article.Importdatum, article.Inhalt.Headline);

            if (count % 50 == 0)
            {
                Log.Debug("Processed {Count} articles so far...", count);
            }
            
            if(count >= MaxCountPerScroll)
            {
                break;
            }
        }

        Log.Information("[CursorClient-Articles] Found {Count} Articles (Dauer: {Duration} ms)",
            count, duration.ElapsedMilliseconds);
    }

    static async Task MediaAccountV2Async(string key)
    {

        var duration = Stopwatch.StartNew();
        using var httpClient = new HttpClient
        {
            BaseAddress = BaseEndpoint,
            DefaultRequestHeaders =
            {
                {"api_key",key}
            }
        };

        var client = new IntializeClient().GetClientV2(httpClient);

        var count = 0;
        var response = client.CreateScroll(RequestDateType.Updatedatum, DateTime.Now.Date.AddDays(-14),
            DateTime.Now.AddMinutes(-5), 50, "&lm_internerZugriff=true");

        while (await response.Next())
        {
            Log.Debug("New Batch >");
            foreach (var item in response.Items)
            {
                Log.Debug("Article:{Id} - Date:{Date}  - Headline:{Headline}", item.Id, item.Importdatum,
                    item.Inhalt.Headline);
            }

            Log.Debug("Waiting for next Batch");
            count += response.Items.Length;
        }

        Log.Information("[V2] Found {Count} Articles (Dauer: {Duration} ms)", count, duration.ElapsedMilliseconds);
    }
}