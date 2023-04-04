using System;
using System.Threading.Tasks;
using Krowiorsch.MediaAccount.Model;
using Krowiorsch.MediaAccount.Model.V2;
using Krowiorsch.MediaAccount.RequestBuilder;

namespace Krowiorsch.MediaAccount
{
    public interface IMediaAccountClient<T>
        where T : class
    {
        /// <summary>
        /// Erzeugt einen Cursor zum durchlaufen der Artikel. Mit Next() wird der nächste Batch abgerufen.
        /// </summary>
        /// <param name="dateType">gibt den Datumstyp an, nach dem selektiert werden soll.</param>
        /// <param name="start">gibt das Startdatum an.</param>
        /// <param name="end">gibt das enddatum ein</param>
        /// <param name="batchSize">gibt an, in welcher Batchgrösse die Daten abgerufen werden sollen</param>
        /// <param name="additionalParameters">ermöglicht die übergabe von benutzerdefinierten Queryparametern</param>
        /// <returns>Gibt ein Cursorobjekt an, in dem man schrittweise duch das result durchlaufen kann.</returns>
        ArticleListScroll<T> CreateScroll(RequestDateType dateType, DateTime start, DateTime end, int batchSize, string additionalParameters);

        /// <summary>ruft einen einzelnen Artikel ab</summary>
        /// <param name="id">gibt die Id des Artikels an.</param>
        /// <returns>gibt den passenden Artikel zurück</returns>
        Task<T> GetByIdAsync(string id);
    }
}