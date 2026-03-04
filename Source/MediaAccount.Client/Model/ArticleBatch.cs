using Krowiorsch.MediaAccount.Model.V2;

namespace Krowiorsch.MediaAccount.Model;

public class ArticleBatch
{
    public ArticleBatch(Article[] articles, long anzahlGesamt, long anzahlVerblieben)
    {
        Articles = articles;
        AnzahlGesamt = anzahlGesamt;
        AnzahlVerblieben = anzahlVerblieben;
    }

    public Article[] Articles { get; }
    public long AnzahlGesamt { get; }
    public long AnzahlVerblieben { get; }
}
