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

        public string CreateInitialUrl(RequestDateType dateType, DateTimeOffset start, DateTimeOffset end, int maxItems = 150, string additionalParameters = null)
        {
            var url = $"{_endpoint}api/v2/Articles?typ={dateType}&von={ToUnixTimeStamp(start)}&bis={ToUnixTimeStamp(end)}&maxItems={maxItems}";

            if(additionalParameters != null)
                url += additionalParameters;

            return url;
        }


        public static long ToUnixTimeStamp(DateTimeOffset dateTime)
        {
            var ts = dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return (long)ts;
        }
    }
}