namespace Krowiorsch.MediaAccount.Model.V3;

public class Auftrag
{
    public string Auftragsnummer { get; set; } = null!;
    public string? Bezeichnung { get; set; } = null!;
    public string? Suchbegriffsname { get; set; }
    public string? SuchbegriffsId { get; set; }
    public string? SuchbegriffsIdExtern { get; set; }
    public string Kundennummer { get; set; } = null!;
}