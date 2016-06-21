using System;
using System.Net.Http;

namespace Krowiorsch.MediaAccount.RequestBuilder
{
    public class ArticleRequestBuilder : BaseRequestBuilder
    {
        public ArticleRequestBuilder(Uri endpoint, string apiKey)
            : base(endpoint, apiKey)
        {
        }

        public HttpRequestMessage Create(RequestDateType dateType, DateTimeOffset start, DateTimeOffset end, int page = 1)
        {
            var message = CreateGet();

            message.RequestUri = new Uri(string.Format("{0}api/v2/Articles?typ={1}&von={2}&bis={3}&page={4}",
                _endpoint,
                dateType,
                ToUnixTimeStamp(start),
                ToUnixTimeStamp(end), 
                page));

            return message;

        }

        public string CreateInitialUrl(RequestDateType dateType, DateTimeOffset start, DateTimeOffset end)
        {
            return string.Format("{0}api/v2/Articles?typ={1}&von={2}&bis={3}",
                _endpoint,
                dateType,
                ToUnixTimeStamp(start),
                ToUnixTimeStamp(end));
        }


        public static long ToUnixTimeStamp(DateTimeOffset dateTime)
        {
            var ts = dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return (long)ts;
        }
    }
}