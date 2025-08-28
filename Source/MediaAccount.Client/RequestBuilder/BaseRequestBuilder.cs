using System.Net.Http;

namespace Krowiorsch.MediaAccount.RequestBuilder
{
    public class BaseRequestBuilder
    {
        protected Uri _endpoint;
        readonly string _userAgent;

        protected BaseRequestBuilder(Uri endpoint)
        {
            _endpoint = endpoint;
            _userAgent = $"MediaAccountClient ({GetType().Assembly.GetName().Version})";
        }

        protected HttpRequestMessage CreateGet()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, _endpoint);
            message.Headers.Add("User-Agent", _userAgent);
            message.Headers.Add("Accept", "application/json");

            return message;
        }
    }
}