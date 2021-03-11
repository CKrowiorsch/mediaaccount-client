using System;

namespace Krowiorsch.MediaAccount.Model.V3
{
    public class Meldung
    {
        public string Id { get; set; }
        public string MedienblattId { get; set; }
        public string MedienblattPdfUrl { get; set; }
        public bool IstMedienblattHaupttreffer { get; set; }
        public string MedienblattHaupttrefferId { get; set; }
        public int? AnzahlSeiten { get; set; }
        public bool ManuellAngelegt { get; set; }
        public DateTime? SendungBeginn { get; set; }
        public DateTime? BeitragBeginn { get; set; }
        public int? BeitragLaenge { get; set; }
        public string Mediengattung { get; set; }
        public string Medienart { get; set; }
        public string PreviewUrl { get; set; }
        public string Url { get; set; }
        public string Seite { get; set; }
        public string PositionAufSeite { get; set; }
        public string Genre { get; set; }
        public string Farbigkeit { get; set; }
        public decimal? Anzeigenaequivalenzwert { get; set; }
        public decimal? GewichteterAnzeigenaequivalenzwert { get; set; }
        public decimal? Artikelgroesse { get; set; }
        public bool? WurdeDigitalisiert { get; set; }
        public DateTime? Lieferdatum { get; set; }
        public DateTime Importdatum { get; set; }
        public DateTime Selektionsdatum { get; set; }
        public DateTime Erscheinungsdatum { get; set; }
        public DateTime Aktualisierungdatum { get; set; }
        public DateTime? Digitalisierungsdatum { get; set; }
        public string[] Schlagworte { get; set; } = new string[0];
        public string Sprache { get; set; }
        public string Herkunftsland { get; set; }
        public string Tonalitaet { get; set; }

        public string UrsprungsId { get; set; }

        public Auftrag Auftrag { get; set; }
        public Inhalt Inhalt { get; set; }
        public Medium Medium { get; set; }
        public Engagement Engagement { get; set; }
        public DateTime? GeloeschtDatum { get; set; }
        public DateTime? StorniertDatum { get; set; }

    }
}