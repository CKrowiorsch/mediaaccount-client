using System;

namespace Krowiorsch.MediaAccount
{
    public class IntializeClient
    {
        public MediaAccountClient GetClient()
        {
            Console.WriteLine("Bitte geben sie den API-Key ein:");
            var apiKey = Console.ReadLine();

            return new MediaAccountClient(apiKey, "http://test.api.media-account2.de");
        } 
    }
}