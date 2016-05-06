namespace Krowiorsch.MediaAccount.Model
{
    public class Auftrag
    {
        public int Auftragsnummer { get; set; }
        
        public string Auftragsbeschreibung { get; set; }
        
        public string Suchbegriffsname { get; set; }
        
        public int SuchbegriffsId { get; set; }
        
        public object SuchbegriffsIdExtern { get; set; }
        
        public int Kundennummer { get; set; }
    }
}