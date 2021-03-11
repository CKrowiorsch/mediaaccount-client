using System;
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
            var client = new IntializeClient().GetClientV2(keyProvider.Provide(), null);

            int count = 0;
            var response = client.CreateScroll(RequestDateType.Aktualisierungsdatum, DateTimeOffset.Now.Date.AddDays(-14), DateTimeOffset.Now.AddMinutes(-5), 100, "&lm_internerZugriff=true");

            while (response.Next().Result)
            {
                Log.Information("New Batch >");
                foreach (var item in response.Items)
                {
                    Log.Information("Article:{Id} - Date:{Date}  - Headline:{Headline}", item.Id, item.Importdatum, item.Inhalt.Headline);
                }

                Log.Debug("Waiting for next Batch");
                count += response.Items.Length;
            }

            Log.Information("Found {Count} Articles", count);

            Console.Read();
            Console.WriteLine();
        }
    }
}
