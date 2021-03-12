using System;

namespace Krowiorsch.MediaAccount
{
    public class IntializeClient
    {
        public MediaAccountClientV2 GetClientV2(string apiKey, Uri baseUri)
        {
            return new MediaAccountClientV2(apiKey, baseUri);
        } 

        public MediaAccountClientV3 GetClientV3(string apiKey, Uri baseUri)
        {
            return new MediaAccountClientV3(apiKey, baseUri);
        } 
    }
}