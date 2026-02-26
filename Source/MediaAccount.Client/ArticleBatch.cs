using Krowiorsch.MediaAccount.Model.V2;

namespace Krowiorsch.MediaAccount;

public class ArticleBatch
{
    public ArticleBatch(Article[] articles, long anzahlGesamt, long anzahlVerbleibed)
    {
        Articles = articles;
        AnzahlGesamt = anzahlGesamt;
        AnzahlVerbleibed = anzahlVerbleibed;
    }

    public Article[] Articles { get; }
    public long AnzahlGesamt { get; }
    public long AnzahlVerbleibed { get; }
}
