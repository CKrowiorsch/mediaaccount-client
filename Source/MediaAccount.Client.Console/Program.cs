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

            var client = new IntializeClient().GetClient();

            //Console.WriteLine("ArticleId");
            //var articleId = Console.ReadLine();

            //var article = client.GetByIdAsync(long.Parse(articleId)).Result;

            var response = client.GetList(RequestDateType.Erscheinungsdatum,
                DateTimeOffset.Now.Date.AddDays(-1),
                DateTimeOffset.Now.Date,
                1).Result;

            response.Items.ForEach(i => Log.Information("Article:{id} - Headline:{headline}", i.Id, i.Inhalt.Headline));
            

            Console.Read();
            Console.WriteLine();
        }
    }
}
