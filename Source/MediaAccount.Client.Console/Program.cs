using System;
using System.Diagnostics;
using Krowiorsch.MediaAccount.RequestBuilder;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Krowiorsch.MediaAccount
{
    public class Program
    {
        public static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}]  {Message} {NewLine}{Exception}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            var keyProvider = new FileApiKeyProvider(new System.IO.FileInfo(@"c:\Daten\MediaAccount.txt"));

            //var client = new IntializeClient().GetClient(keyProvider.Provide(), new Uri("http://api-test.maazure.dev.local"));

            for (var i = 0; i < 5; i++)
            {
                Log.Information("Start Iteration-{Iteration} - {Key}", i, keyProvider.Provide());
                MediaAccountV2(keyProvider.Provide());
                MediaAccountV3(keyProvider.Provide());
            }

            Console.Read();
            Console.WriteLine();
        }

        static void MediaAccountV3(string key)
        {
            var duration = Stopwatch.StartNew();
            var client = new IntializeClient().GetClientV3(key, null);

            int count = 0;
            var response = client.CreateScroll(RequestDateType.Aktualisierungsdatum, DateTimeOffset.Now.Date.AddDays(-14),
                DateTimeOffset.Now.AddMinutes(-5), 50, "&lm_internerZugriff=true");

            while (response.Next().Result)
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

        static void MediaAccountV2(string key)
        {
            var duration = Stopwatch.StartNew();
            var client = new IntializeClient().GetClientV2(key, null);

            int count = 0;
            var response = client.CreateScroll(RequestDateType.Updatedatum, DateTimeOffset.Now.Date.AddDays(-14),
                DateTimeOffset.Now.AddMinutes(-5), 50, "&lm_internerZugriff=true");

            while (response.Next().Result)
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
