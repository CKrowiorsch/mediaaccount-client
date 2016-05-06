using System;

namespace Krowiorsch.MediaAccount
{
    public class IntializeClient
    {
        public MediaAccountClient GetClient()
        {
            Console.WriteLine("Bitte geben sie den API-Key ein:");
            var apiKey = Console.ReadLine();

            return new MediaAccountClient(apiKey, new Uri("http://test.api.media-account2.de"));
        } 
    }
}