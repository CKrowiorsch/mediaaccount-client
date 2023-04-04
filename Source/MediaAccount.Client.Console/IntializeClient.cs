using System;

namespace Krowiorsch.MediaAccount;

public class IntializeClient
{
    public MediaAccountClientV2 GetClientV2(string apiKey, Uri baseUri) => new(apiKey, baseUri);
    public MediaAccountClientV3 GetClientV3(string apiKey, Uri baseUri) => new(apiKey, baseUri);
}