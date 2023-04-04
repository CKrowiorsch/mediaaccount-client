using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Krowiorsch.MediaAccount.RequestBuilder;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Krowiorsch.MediaAccount
{
    public class Program
    {
        public static async Task Main()
        {

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}]  {Message} {NewLine}{Exception}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            var keyProvider = new FileApiKeyProvider(new System.IO.FileInfo(@"c:\Daten\MediaAccount.txt"));

            for (var i = 0; i < 5; i++)
            {
                Log.Information("Start Iteration-{Iteration} - {Key}", i, keyProvider.Provide());
                await MediaAccountV2Async(keyProvider.Provide());
                await MediaAccountV3Async(keyProvider.Provide());
            }

            Console.Read();
            Console.WriteLine();
        }

        static async Task MediaAccountV3Async(string key)
        {
            var duration = Stopwatch.StartNew();
            var client = new IntializeClient().GetClientV3(key, null);

            var count = 0;
            var response = client.CreateScroll(RequestDateType.Aktualisierungsdatum, DateTimeOffset.Now.Date.AddDays(-14),
                DateTimeOffset.Now.AddMinutes(-5), 50, "&lm_internerZugriff=true");

            while (await response.Next().ConfigureAwait(false))
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

            Log.Information("[V3] Found {Count} Articles (Dauer: {Duration} ms)", count, duration.ElapsedMilliseconds);
        }

        static async Task MediaAccountV2Async(string key)
        {
            var duration = Stopwatch.StartNew();
            var client = new IntializeClient().GetClientV2(key, null);

            var count = 0;
            var response = client.CreateScroll(RequestDateType.Updatedatum, DateTimeOffset.Now.Date.AddDays(-14),
                DateTimeOffset.Now.AddMinutes(-5), 50, "&lm_internerZugriff=true");

            while (await response.Next().ConfigureAwait(false))
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
}
