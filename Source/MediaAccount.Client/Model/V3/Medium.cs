namespace Krowiorsch.MediaAccount.Model.V3;

public class Medium
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Land { get; set; }
    public string PublizistischeEinheit { get; set; }
    public long Reichweite { get; set; }
    public long? Visits { get; set; }
    public long? PageImpressions { get; set; }
    public long? VerkaufteAuflage { get; set; }
    public long? VerbreiteteAuflage { get; set; }
    public long? GedruckteAuflage { get; set; }
    public string Bundesland { get; set; }
    public string Nielsengebiet { get; set; }
    public string Themengebiet { get; set; }
    public string Homepage { get; set; }
    public decimal? WerbepreisProSekunde { get; set; }
    public string IvwNummer { get; set; }
    public string IvwNameOnline { get; set; }
    public string ZimpelNr { get; set; }
    public decimal? WerbepreisC1 { get; set; }
    public decimal? WerbepreisC2 { get; set; }
    public decimal? WerbepreisC3 { get; set; }
    public decimal? WerbepreisC4 { get; set; }
    public string TvSenderName { get; set; }
    public string SendungsName { get; set; }
    public int? SendungsLaenge { get; set; }
    public string Ausgabennummer { get; set; }
    public string Erscheinungszyklus { get; set; }
    public long? Satzhoehe { get; set; }
    public long? Satzbreite { get; set; }
    public string Sprache { get; set; }
    public Redaktion Redaktion { get; set; }
    public Verlag Verlag { get; set; }

        
}