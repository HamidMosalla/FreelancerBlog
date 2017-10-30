namespace FreelancerBlog.Core.Domain
{
    public class ArticleArticleTag
    {
        public int ArticleId { get; set; }
        public int ArticleTagId { get; set; }
        public Article Article { get; set; }
        public ArticleTag ArticleTag { get; set; }



    }
}
