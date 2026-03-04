using System;
using System.Net.Http;

namespace Krowiorsch.MediaAccount;

public class IntializeClient
{
    public MediaAccountClientV2 GetClientV2(HttpClient client) => new(client);

    public MediaAccountCursorClient GetCursorClient(HttpClient client) => new(client);
}