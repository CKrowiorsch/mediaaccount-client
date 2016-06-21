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

            int count = 0;

            var response = client.GetList(RequestDateType.Importdatum,
                //DateTimeOffset.Now.Date.AddDays(-1),
                DateTimeOffset.Now.AddHours(-2),
                DateTimeOffset.Now,
                0).Result;

            response.Items.ForEach(i => Log.Information("Article:{id} - Headline:{headline}", i.Id, i.Inhalt.Headline));

            count += response.Items.Count();

            while(response.Next().Result)
            {
                response.Items.ForEach(i => Log.Information("Article:{id} - Headline:{headline}", i.Id, i.Inhalt.Headline));

                Log.Debug("Waiting for next Batch");
                count += response.Items.Count();
            }
                
            

            Console.Read();
            Console.WriteLine();
        }
    }
}
