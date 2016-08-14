using Krowiorsch.MediaAccount.Model;
using Newtonsoft.Json.Linq;

namespace Krowiorsch.MediaAccount
{
    internal class ArticleListDeserializer
    {
        public bool DeserializeInto(string json, ArticleListScroll scroll)
        {
            var token = JObject.Parse(json);

            scroll.NextPageLink = token["NextPageLink"].Value<string>();
            scroll.Count = token["Count"].Value<int>();
            scroll.Items = token["Items"].ToObject<Article[]>();

            return true;
        }
    }
}