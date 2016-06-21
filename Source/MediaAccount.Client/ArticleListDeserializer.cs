using Krowiorsch.MediaAccount.Model;
using Newtonsoft.Json;

namespace Krowiorsch.MediaAccount
{
    public class ArticleListDeserializer
    {
        public ArticleListScroll Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<ArticleListScroll>(json);
        } 
    }
}