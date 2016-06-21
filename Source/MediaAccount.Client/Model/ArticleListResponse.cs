namespace Krowiorsch.MediaAccount.Model
{
    public class ArticleListResponse
    {
        public Article[] Items { get; set; } 

        public int Count { get; set; }

        public string NextPageLink { get; set; }
    }
}