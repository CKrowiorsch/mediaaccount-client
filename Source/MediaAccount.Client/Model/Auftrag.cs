namespace Krowiorsch.MediaAccount.Model
{
    /// <summary>
    /// Bei Landaumedia wir dein Auftrag mit mehreren Aufträgen geschaltet. Diese Objekt gibt die grundlegenden Daten für den Auftrag an.
    /// </summary>
    public class Auftrag
    {
        /// <summary>auftragsnummer für den der Artikel gefunden wurde.</summary>
        public int Auftragsnummer { get; set; }
        
        /// <summary> Auftragsbeschreibung für den Auftrag </summary>
        public string Auftragsbeschreibung { get; set; }
        
        /// <summary> Suchbegriffsname für den der Artikel gefunden wurde</summary>
        public string Suchbegriffsname { get; set; }
        
        /// <summary> interne Landaumedia Suchbegriffs Id </summary>
        public int SuchbegriffsId { get; set; }
        
        /// <summary>externe Id für den Suchbegriff</summary>
        public string SuchbegriffsIdExtern { get; set; }
        
        /// <summary>Kundennummer des Kunden </summary>
        public int Kundennummer { get; set; }
    }
}