using System;
using System.Net.Http;
using Krowiorsch.MediaAccount.Model;

namespace Krowiorsch.MediaAccount.RequestBuilder
{
    public class ArticleScrollBuilder : BaseRequestBuilder
    {
        public ArticleScrollBuilder(Uri endpoint, string apiKey)
            : base(endpoint, apiKey)
        {
        }

        public HttpRequestMessage Create(ArticleListScroll scroll)
        {
            var message = CreateGet();
            message.RequestUri = new Uri(scroll.NextPageLink);
            return message;

        }
    }
}