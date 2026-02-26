using System;
using System.Net.Http;

namespace Krowiorsch.MediaAccount;

public class IntializeClient
{
    public MediaAccountClientV2 GetClientV2(HttpClient client) => new(client);
    public MediaAccountClientV3 GetClientV3(string apiKey, Uri baseUri) => new(apiKey, baseUri);
}