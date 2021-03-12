using System;
using System.Net.Http;
using Krowiorsch.MediaAccount.Model;
using Krowiorsch.MediaAccount.Model.V2;

namespace Krowiorsch.MediaAccount.RequestBuilder
{
    public class ArticleScrollBuilder : BaseRequestBuilder
    {
        public ArticleScrollBuilder(Uri endpoint, string apiKey)
            : base(endpoint, apiKey)
        {
        }

        public HttpRequestMessage Create<T>(ArticleListScroll<T> scroll)
            where T : class
        {
            var message = CreateGet();
            message.RequestUri = new Uri(scroll.NextPageLink);
            return message;

        }
    }
}