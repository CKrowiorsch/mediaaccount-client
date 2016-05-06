using Krowiorsch.MediaAccount.Model;

namespace Krowiorsch.MediaAccount
{
    public class MediaAccountClient
    {
        string _apiKey;
        string _endpoint;

        public MediaAccountClient(string apiKey, string endpoint)
        {
            _apiKey = apiKey;
            _endpoint = endpoint;
        }

        public Article GetById(long id)
        {
            return null;
        }

    }
}