using System;
using System.Threading.Tasks;

namespace Krowiorsch.MediaAccount.Model
{
    public class ArticleListScroll : IDisposable
    {
        MediaAccountClient _client;

        internal ArticleListScroll(MediaAccountClient client)
        {
            _client = client;
        }

        public Article[] Items { get; set; }

        public int Count { get; set; }

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