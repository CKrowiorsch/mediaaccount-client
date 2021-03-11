using Krowiorsch.MediaAccount.Model;
using Newtonsoft.Json.Linq;
using System;
using Krowiorsch.MediaAccount.Model.V2;

namespace Krowiorsch.MediaAccount
{
    internal class ArticleListDeserializer
    {
        public bool DeserializeInto<T>(string json, ArticleListScroll<T> scroll)
            where T : class
        {
            var token = JObject.Parse(json);

            scroll.NextPageLink = token["NextPageLink"].Value<string>();
            scroll.Count = token["Count"].Value<int>();
            scroll.Items = token["Items"].ToObject<Article[]>();

            foreach(var item in scroll.Items)
            {
                Repair(item);
            }

            return true;
        }

        /// <summary>dient dazu, fehlerhaft Jsonwerte vom Server zu korrigieren</summary>
        void Repair(Article article)
        {
            article.Lieferdatum = RepairDate(article.Lieferdatum);
            article.UpdateDatum = RepairDate(article.UpdateDatum);
            article.Digitalisierungsdatum = RepairDate(article.Digitalisierungsdatum);
        }

        static DateTime? RepairDate(DateTime? datetime)
        {
            if (datetime.HasValue && datetime.Equals(DateTime.MinValue))
                return null;

            return datetime;
        }
    }
}