namespace Krowiorsch.MediaAccount.Model.V2;

/// <summary>Gibt den textuellen Inhalt des Artikels an</summary>
public class Inhalt
{
    /// <summary>Headline des Artikels </summary>
    public string? Headline { get; set; }

    /// <summary>Subheadline des Artikels </summary>
    public string? Subheadline { get; set; }

    /// <summary>Kurzfassung des Artikels </summary>
    /// <remarks>wird nur zur Verfügung gestellt, wenn es extra bestellt wird.</remarks>
    public string? Abstract { get; set; }

    /// <summary>Volltext des Artikels</summary>
    /// <remarks>wird nur zur Verfügung gestellt, wenn das Lizenzrecht es erlaubt</remarks>
    public string? Text { get; set; }

    /// <summary>Kurztext des Artikels (Snippet)</summary>
    public string? Previewtext { get; set; }

    /// <summary>Autor des Artikels</summary>
    public string? Autor { get; set; }

    /// <summary> das interne Artikeldokument (Darstellung der internen Informationen)</summary>
    public string? Artikeldokument { get; set; }
}