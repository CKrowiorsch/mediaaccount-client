namespace Krowiorsch.MediaAccount.Model.V3;

public class Engagement
{
    public long? Likes { get; set; }
    public long? Dislikes { get; set; }
    public long? AnzahlKommentare { get; set; }
    public long? Shares { get; set; }
    public long? VideoAufrufe { get; set; }
    public double? Engagementrate { get; set; }
    public double? EngagementrateInProzent { get; set; }
    public DateTime? AbrufDatum { get; set; }
}