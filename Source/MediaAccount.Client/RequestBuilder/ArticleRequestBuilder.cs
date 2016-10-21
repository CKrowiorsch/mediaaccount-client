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

        public string CreateInitialUrl(RequestDateType dateType, DateTimeOffset start, DateTimeOffset end, int maxItems = 150)
        {
            return string.Format("{0}api/v2/Articles?typ={1}&von={2}&bis={3}&maxItems={4}",
                _endpoint,
                dateType,
                ToUnixTimeStamp(start),
                ToUnixTimeStamp(end),
                maxItems);
        }


        public static long ToUnixTimeStamp(DateTimeOffset dateTime)
        {
            var ts = dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return (long)ts;
        }
    }
}