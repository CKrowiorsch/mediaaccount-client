using System;
using System.Linq;
using Krowiorsch.MediaAccount.RequestBuilder;
using Serilog;

namespace Krowiorsch.MediaAccount
{
    public class Program
    {
        public static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.LiterateConsole()
                .CreateLogger();

            var keyProvider = new FileApiKeyProvider(new System.IO.FileInfo(@"c:\Daten\MediaAccount.txt"));
            var baseUri = new Uri("http://api.media-account2.de");

            var client = new IntializeClient().GetClient(keyProvider.Provide(), baseUri);

            int count = 0;

            var response = client.CreateScroll(RequestDateType.Importdatum, DateTimeOffset.Now.Date.AddDays(-14), DateTimeOffset.Now.AddMinutes(-5));

            while (response.Next().Result)
            {
                response.Items.ForEach(i => Log.Information("Article:{id} - Headline:{headline}", i.Id, i.Inhalt.Headline));

                Log.Debug("Waiting for next Batch");
                count += response.Items.Count();
            }

            Log.Information("Found {count} Articles", count);

            Console.Read();
            Console.WriteLine();
        }
    }
}
