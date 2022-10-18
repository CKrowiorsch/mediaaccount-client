using System;
using System.Net.Http;

namespace Krowiorsch.MediaAccount.RequestBuilder
{
    public class V2ArticleRequestBuilder : BaseRequestBuilder
    {
        public V2ArticleRequestBuilder(Uri endpoint, string apiKey)
            : base(endpoint, apiKey)
        {
        }

        public string CreateInitialUrl(RequestDateType dateType, DateTimeOffset start, DateTimeOffset end, int maxItems = 150, string additionalParameters = null)
        {
            if (dateType == RequestDateType.Aktualisierungsdatum)
                dateType = RequestDateType.Updatedatum;

            var url = $"{_endpoint}api/v2/Articles?typ={dateType}&von={ToUnixTimeStamp(start)}&bis={ToUnixTimeStamp(end)}&maxItems={maxItems}";

            if (additionalParameters != null)
                url += additionalParameters;

            return url;
        }

        public static long ToUnixTimeStamp(DateTimeOffset dateTime)
        {
            var ts = dateTime.Subtract(new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds;
            return (long)ts;
        }
    }

    public class V3ArticleRequestBuilder : BaseRequestBuilder
    {
        public V3ArticleRequestBuilder(Uri endpoint, string apiKey)
            : base(endpoint, apiKey)
        {
        }

        public string CreateInitialUrl(RequestDateType dateType, DateTimeOffset start, DateTimeOffset end, int maxItems = 150, string additionalParameters = null)
        {
            if (dateType == RequestDateType.Updatedatum)
                dateType = RequestDateType.Aktualisierungsdatum;

            var url = $"{_endpoint}api/v3/meldungen?datum={dateType}&von={ToUnixTimeStamp(start)}&bis={ToUnixTimeStamp(end)}&anzahl={maxItems}";

            if (additionalParameters != null)
                url += additionalParameters;

            return url;
        }


        public static long ToUnixTimeStamp(DateTimeOffset dateTime)
        {
            var ts = dateTime.Subtract(new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds;
            return (long)ts;
        }
    }
}