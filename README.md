# MediaAccount Client

[![Build status](https://ci.appveyor.com/api/projects/status/69ia7yqncekogjdx?svg=true)](https://ci.appveyor.com/project/ChristianKrowiorsch/mediaaccount-client)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/92eec029e6d6437cb44ef7c8f784d8d3)](https://app.codacy.com/gh/CKrowiorsch/mediaaccount-client/dashboard)

Ein Nuget Package für den Zugriff auf die Api des MediaAccount. <http://api.media-account.de/>

# Benutzung

## Einmaliger Abruf

```csharp

var client = new MediaAccountClientV2("<APIKEY>");

var start = DateTime.Now.Date.AddDays(-14);
var ende = DateTime.Now;

var response = client.CreateScroll(RequestDateType.Updatedatum, start, ende);

while (await response.Next()) 
{
    foreach (var item in response.Items)
    {
        // verarbeite
    }
}

```

## Streaming

```csharp

var client = new MediaAccountClientV2("<APIKEY>");
var token = new CancellationToken();
var start = DateTime.Now.Date.AddDays(-14);

while(!token.IsCancellationRequested)
{
    var artikel = LadeArtikelFuerZeitraum(client, start);
    if (artikel.Any()) {
        
        // verarbeite article
        
        start = artikel.Max(t => t.UpdateDatum);
    }
    
    Task.Delay(Timespan.FromSeconds(60), token);
}


static async Task<Article[]> LadeArtikelFuerZeitraum(MediaAccountClientV2 client, DateTime start) 
{
    var ende = DateTime.Now;
    var response = client.CreateScroll(RequestDateType.Updatedatum, start, ende);

    vat list = new List<Article>();
    while (await response.Next()) 
    {
        list.AddRange(response.Items);
    }

    return list.ToArray();
}


```
