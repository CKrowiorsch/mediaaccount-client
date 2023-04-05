using Newtonsoft.Json;

namespace Krowiorsch.MediaAccount.Model.V2;

public class Article
{
    /// <summary> Eindeutiger Identifier des Artikel</summary>
    public string Id { get; set; } = null!;

    /// <summary> gibt den Auftrag+Suchbegriff an, für den der Artikel gefunden wurde. </summary>
    public Auftrag Auftrag { get; set; } = null!;

    /// <summary> wenn es für den Artikel ein Medienblatt gibt, gibt dieser Wert den Anzahl der Seiten an </summary>
    public int? AnzahlSeiten { get; set; }

    /// <summary> eindeutige Id des Medienblatts</summary>
    public string? MedienblattId { get; set; }

    /// <summary> Link zu dem Medienblatt - es wird ein Pdf geliefert</summary>
    public string? MedienblattLink { get; set; }

    /// <summary> Uhrzeit wann die Sendung beginnt </summary>
    public string? SendungsBeginn { get; set; }

    public string? Beitragsstart { get; set; }
    public int? Beitragslaenge { get; set; }

    /// <summary> Mediengattung (PRINT, TV usw.) </summary>
    public string Mediengattung { get; set; } = null!;

    /// <summary> Mediengattung (Internet-Publikation, Anzeigenblatt usw) </summary>
    public string Medienart { get; set; } = null!;

    /// <summary> Link auf ein Preview (Radio-Preview, TV-Preview usw) </summary>
    public string? PreviewLink { get; set; }

    public string? Deeplink { get; set; }
    public string? Seite { get; set; }
    public string? PositionAufSeite { get; set; }
    public string? Genre { get; set; }
    public string? Farbigkeit { get; set; }
    public double? Anzeigenaequivalenzwert { get; set; }
    public double? Artikelgroesse { get; set; }
    public bool IsHaupttreffer { get; set; }
    public int? HaupttrefferId { get; set; }

    /// <summary> return, if the article is digital available </summary>
    public bool? IsDigitized { get; set; }

    public DateTime? Lieferdatum { get; set; }
    public DateTime Importdatum { get; set; }
    public DateTime Selektionsdatum { get; set; }
    public DateTime Erscheinungsdatum { get; set; }

    public DateTime? UpdateDatum { get; set; }
    public DateTime? Digitalisierungsdatum { get; set; }

    public Inhalt Inhalt { get; set; } = null!;
    public Publikation Publikation { get; set; } = null!;

    /// <summary>gibt die an dem Artikeldefinierten Tags an.</summary>
    public string[] Tags { get; set; } = Array.Empty<string>();

    /// <summary> Herkunftsland des Artikels </summary>
    public string? Herkunftsland { get; set; }

    /// <summary> Sprache des Artikels </summary>
    public string? Sprache { get; set; }

    /// <summary> Tonalitaet des Artikels </summary>
    public string? Tonalitaet { get; set; }

    /// <summary> Statistische Daten </summary>
    public Engagement? Engagement { get; set; }

    /// <summary> GAAW aus der Analyse </summary>
    public decimal? GewichteterAnzeigenaequivalenzwert { get; set; }

    [JsonExtensionData(ReadData = true, WriteData = false)]
    public Dictionary<string, object> Unmapped { get; set; } = new();
}