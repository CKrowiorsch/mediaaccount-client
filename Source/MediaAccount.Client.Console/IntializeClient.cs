using System;

namespace Krowiorsch.MediaAccount
{
    public class IntializeClient
    {
        public MediaAccountClient GetClient(string apiKey, Uri baseUri)
        {
            return new MediaAccountClient(apiKey, baseUri, ApiVersions.Version3);
        } 
    }
}