#if NETSTANDARD2_1_OR_GREATER

using Krowiorsch.MediaAccount.Model.V2;

namespace Krowiorsch.MediaAccount;

public static class MediaAccountCursorClientExtensions
{
    public static async IAsyncEnumerable<ArticleBatch> GetArticleBatchesAsync(
        this MediaAccountCursorClient client,
        DateTime start,
        int batchSize,
        IDictionary<string, string>? parameter = null,
        CancellationToken cancellation = default)
    {
        var response = await client.SendRequest(start, batchSize, parameter, cancellation);

        if (response.Liste != null && response.Liste.Length > 0)
        {
            yield return new ArticleBatch(
                response.Liste,
                response.AnzahlGesamt,
                response.AnzahlVerbleibed);
        }

        while (response.NaechsterAbrufUrl != null)
        {
            response = await client.SendRequest(response, cancellation);

            if (response.Liste != null && response.Liste.Length > 0)
            {
                yield return new ArticleBatch(
                    response.Liste,
                    response.AnzahlGesamt,
                    response.AnzahlVerbleibed);
            }
        }
    }

    public static async IAsyncEnumerable<Article> GetArticlesAsync(
        this MediaAccountCursorClient client,
        DateTime start,
        int batchSize,
        IDictionary<string, string>? parameter = null,
        CancellationToken cancellation = default)
    {
        await foreach (var batch in client.GetArticleBatchesAsync(start, batchSize, parameter, cancellation))
        {
            foreach (var article in batch.Articles)
            {
                yield return article;
            }
        }
    }
}

#endif
