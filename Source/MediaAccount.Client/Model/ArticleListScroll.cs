using System;
using System.Threading.Tasks;
using Krowiorsch.MediaAccount.Model.V2;

namespace Krowiorsch.MediaAccount.Model
{
    /// <summary>
    /// definiert einen Cursor für das Result
    /// </summary>
    public class ArticleListScroll<T> : IDisposable
        where T : class
    {
        readonly IMediaAccountClient<T> _client;

        readonly Func<ArticleListScroll<T>, Task<bool>> _onNext;

        internal ArticleListScroll(IMediaAccountClient<T> client, Func<ArticleListScroll<T>, Task<bool>> onNext)
        {
            _client = client;
            _onNext = onNext;
        }

        /// <summary> gibt alle Artikel an</summary>
        public Article[] Items { get; set; }

        /// <summary> gibt das gesamtergebnis an</summary>
        public int Count { get; set; }

        /// <summary>Link auf das nächste Ergebnisset</summary>
        public string NextPageLink { get; set; }

        public async Task<bool> Next()
        {
            return await _onNext(this);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_client is IDisposable disposable)
                disposable.Dispose();
        }
    }
}