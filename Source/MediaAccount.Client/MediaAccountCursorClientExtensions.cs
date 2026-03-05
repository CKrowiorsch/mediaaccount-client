#if NETSTANDARD2_1_OR_GREATER

using System.Runtime.CompilerServices;
using Krowiorsch.MediaAccount.Model;
using Krowiorsch.MediaAccount.Model.V2;

namespace Krowiorsch.MediaAccount;

public static class MediaAccountCursorClientExtensions
{
    public static async IAsyncEnumerable<ArticleBatch> GetArticleBatchesAsync(
        this MediaAccountCursorClient client,
        string apiKey,
        DateTime start,
        int batchSize,
        IDictionary<string, string>? parameter = null,
        [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        var response = await client.SendRequest(apiKey, start, batchSize, parameter, cancellation);

        if (response.Liste is { Length: > 0 })
        {
            yield return new ArticleBatch(
                response.Liste,
                response.AnzahlGesamt,
                response.AnzahlVerblieben);
        }

        while (!string.IsNullOrEmpty(response.NaechsterAbrufUrl))
        {
            response = await client.SendRequest(response, cancellation);

            if (response.Liste is { Length: > 0 })
            {
                yield return new ArticleBatch(
                    response.Liste,
                    response.AnzahlGesamt,
                    response.AnzahlVerblieben);
            }
        }
    }

    public static async IAsyncEnumerable<Article> GetArticlesAsync(
        this MediaAccountCursorClient client,
        string apiKey,
        DateTime start,
        int batchSize,
        IDictionary<string, string>? parameter = null,
        [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        await foreach (var batch in client.GetArticleBatchesAsync(apiKey, start, batchSize, parameter, cancellation))
        {
            foreach (var article in batch.Articles)
            {
                yield return article;
            }
        }
    }
}

#endif
