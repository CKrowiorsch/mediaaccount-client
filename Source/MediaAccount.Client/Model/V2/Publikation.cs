namespace Krowiorsch.MediaAccount.Model.V2;

public class Publikation
{
    public string PublikationId { get; set; }
    public string Publikationsname { get; set; }
    public string TvSenderName { get; set; }
    public string PublikationsLand { get; set; }
    public int Reichweite { get; set; }
    public long? Visits { get; set; }
    public long? PageImpressions { get; set; }
    public decimal? VerkaufteAuflage { get; set; }
    public decimal? VerbreiteteAuflage { get; set; }
    public decimal? GedruckteAuflage { get; set; }
    public string Bundesland { get; set; }
    public string Nielsengebiet { get; set; }
    public decimal? WerbepreisProSekunde { get; set; }
    public string IvwNummer { get; set; }
    public string IvwNameOnline { get; set; }
    public string ZimpelNr { get; set; }
    public decimal? WerbepreisC1 { get; set; }
    public decimal? WerbepreisC2 { get; set; }
    public decimal? WerbepreisC3 { get; set; }
    public decimal? WerbepreisC4 { get; set; }
    public decimal? Bannerpreis { get; set; }
    public decimal? BannerpreisBasis { get; set; }

    public string PublizistischeEinheit { get; set; }

    public string Themengebiet { get; set; }
    public string Homepage { get; set; }

    public string SendungsName { get; set; }
    public decimal? Sendungslaenge { get; set; }
    public string Ausgabennummer { get; set; }
    public string Erscheinungszyklus { get; set; }
    public string ErscheinungszyklusEnglisch { get; set; }
    public int? Satzhoehe { get; set; }
    public int? Satzbreite { get; set; }
    public string Sprache { get; set; }
    public string LandNameEnglisch { get; set; }
    public Verlag Verlag { get; set; }
    public Redaktion Redaktion { get; set; }
}