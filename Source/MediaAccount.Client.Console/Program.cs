using System;
using System.Linq;
using Krowiorsch.MediaAccount.RequestBuilder;

namespace Krowiorsch.MediaAccount
{
    public class Program
    {

        public static void Main()
        {
            var client = new IntializeClient().GetClient();

            //Console.WriteLine("ArticleId");
            //var articleId = Console.ReadLine();

            //var article = client.GetByIdAsync(long.Parse(articleId)).Result;

            var articles = client.GetList(RequestDateType.Erscheinungsdatum,
                DateTimeOffset.Now.Date.AddDays(-1),
                DateTimeOffset.Now.Date,
                1).Result;


            Console.WriteLine(articles.Count());

            Console.Read();
            Console.WriteLine();
        }
    }
}
