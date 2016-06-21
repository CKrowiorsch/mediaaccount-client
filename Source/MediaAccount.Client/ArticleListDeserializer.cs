using System;
using System.Linq;
using Krowiorsch.MediaAccount.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Krowiorsch.MediaAccount
{
    public class ArticleListDeserializer
    {
        public ArticleListScroll Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<ArticleListScroll>(json);
        }

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