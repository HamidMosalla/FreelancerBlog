namespace FreelancerBlog.Core.DomainModels
{
    public class ArticleRating
    {
        public int ArticleRatingId { get; set; }
        public double ArticleRatingScore { get; set; }

        //[ForeignKey("UserIDfk")]   
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string UserIDfk { get; set; }

        public int ArticleIDfk { get; set; }
        //[ForeignKey("ArticleIDfk")]   
        public virtual Article Article { get; set; }
    }
}
