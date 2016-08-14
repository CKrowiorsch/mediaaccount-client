using System;
using System.Threading.Tasks;

namespace Krowiorsch.MediaAccount.Model
{
    /// <summary>
    /// definiert einen Cursor für das Result
    /// </summary>
    public class ArticleListScroll : IDisposable
    {
        MediaAccountClient _client;

        internal ArticleListScroll(MediaAccountClient client)
        {
            _client = client;
        }

        /// <summary> gibt alle Artikel an</summary>
        public Article[] Items { get; set; }

        /// <summary> gibt das gesamtergebnis an</summary>
        public int Count { get; set; }

        /// <summary>Link auf das nächste Ergebnisset</summary>
        public string NextPageLink { get; set; }

        public async Task<bool> Next()
        {
            return await _client.MoveScroll(this);
        }

        public void Dispose()
        {
            if (_client != null)
                _client.Dispose();
        }
    }
}