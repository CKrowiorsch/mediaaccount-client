using System;

namespace Krowiorsch.MediaAccount
{
    public class Program
    {

        public static void Main()
        {
            MediaAccountClient client = new IntializeClient().GetClient();

            Console.WriteLine("ArticleId");
            var articleId = Console.ReadLine();

            var article = client.GetByIdAsync(long.Parse(articleId)).Result;

            Console.WriteLine(article.Auftrag);

            Console.Read();
            Console.WriteLine();
        }
    }
}
