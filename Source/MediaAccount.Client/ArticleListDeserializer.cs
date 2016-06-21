using Krowiorsch.MediaAccount.Model;
using Newtonsoft.Json;

namespace Krowiorsch.MediaAccount
{
    public class ArticleListDeserializer
    {
        public ArticleListResponse Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<ArticleListResponse>(json);
        } 
    }
}