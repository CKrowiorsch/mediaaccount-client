using System.ComponentModel;

namespace Krowiorsch.MediaAccount.Model.V2;

public class Medienspiegel
{
    [Description("Die Id des Medienspiegels.")]
    public string Id { get; set; }

    [Description("Gibt den Status der Meldung im Medienspiegel an.")]
    public string Status { get; set; }

    [Description("Kategorie in der die Meldung im Medienspiegel zugeordnet ist.")]
    public string Kategorie { get; set; }
}