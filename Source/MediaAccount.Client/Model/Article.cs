namespace Krowiorsch.MediaAccount.Model
{
    public class Article
    {
        /// <summary> </summary>
        public string Id { get; set; }

        /// <summary>  </summary>
        public Auftrag Auftrag { get; set; }

        public int? AnzahlSeiten { get; set; }
        public string MedienblattId { get; set; }
        public string MedienblattLink { get; set; }
        
        public object SendungsBeginn { get; set; }
        public object Beitragsstart { get; set; }
        public object Beitragslaenge { get; set; }
        public string Mediengattung { get; set; }
        public string Medienart { get; set; }
        public object PreviewLink { get; set; }
        public string Deeplink { get; set; }
        public string Seite { get; set; }
        public string PositionAufSeite { get; set; }
        public object Genre { get; set; }
        public string Farbigkeit { get; set; }
        public double? Anzeigenaequivalenzwert { get; set; }
        public double? Artikelgroesse { get; set; }
        public bool IsHaupttreffer { get; set; }
        public int? HaupttrefferId { get; set; }
        
        /// <summary> return, if the article is digital available </summary>
        // TODO: why nullable
        public bool? IsDigitized { get; set; }

        public string Lieferdatum { get; set; }
        public string Importdatum { get; set; }
        public string Selektionsdatum { get; set; }
        public string Erscheinungsdatum { get; set; }
        public string UpdateDatum { get; set; }
        public string Digitalisierungsdatum { get; set; }
        
        public Inhalt Inhalt { get; set; }
        public Publikation Publikation { get; set; }
        
        public string[] Tags { get; set; }
    }
}