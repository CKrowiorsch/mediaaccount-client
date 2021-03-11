using System;

namespace Krowiorsch.MediaAccount.RequestBuilder
{
    public enum RequestDateType
    {
        Importdatum,
        Erscheinungsdatum,
        Selektionsdatum,
        Lieferdatum,

        [Obsolete("Updatedatum wird ab v3 nicht mehr benutzt - bitte AktualisierungsDatum nutzen")]
        Updatedatum,
        DigitalisierungsDatum,
        Aktualisierungsdatum
    }
}